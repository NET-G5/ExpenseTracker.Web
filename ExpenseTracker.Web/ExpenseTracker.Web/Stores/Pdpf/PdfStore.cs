using ExpenseTracker.Web.Requests.Transfer;
using ExpenseTracker.Web.Stores.Transfer;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseTracker.Web.Stores.Pdpf;

public class PdfStore
{
    private readonly ITransferStore _transferStore;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public PdfStore(IHttpContextAccessor httpContextAccessor, ITransferStore transferStore)
    {
        _transferStore = transferStore ?? throw new ArgumentNullException(nameof(transferStore));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    public async Task<MemoryStream> PdfDownload(GetTransfersRequest request)
    {
        var token = _httpContextAccessor.HttpContext?.Request?.Cookies["JWT"];

        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var username = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        var transfers = await _transferStore.GetAll(request);

        PdfDocument doc = new PdfDocument();
        PdfPage page = doc.Pages.Add();
        PdfGraphics graphics = page.Graphics;
        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
        PdfFont fontForTitle = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

        var imgPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "company-img.png");

        FileStream imageStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
        PdfBitmap image = new PdfBitmap(imageStream);

        float imageX = (page.GetClientSize().Width - image.Width + 60) / 2;
        float imageY = 0;
        float titleY = imageY + image.Height + 5;

        graphics.DrawImage(image, imageX, imageY);
        graphics.DrawString("Transfers", fontForTitle, PdfBrushes.Black, new PointF(215, titleY));

        PdfGrid pdfGrid = new PdfGrid();
        List<object> data = [];

        decimal totalAmount = 0;
        foreach (var item in transfers)
        {
            totalAmount += item.Amount;
            data.Add(
               new
               {
                   item.Id,
                   item.Title,
                   Walletname = item.CategoryName,
                   Categoryname = item.CategoryName,
                   Notes = item.Notes ?? "No notes",
                   item.Amount,
                   Date = item.Date.ToString("dd/MM/yyyy")

               });
        }
        IEnumerable<object> dataTable = data;
        pdfGrid.DataSource = dataTable;
        pdfGrid.Columns[0].Width = 25;
        pdfGrid.Columns[5].Width = 30;
        pdfGrid.Columns[6].Width = 50;

        pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

        float tableY = imageY + image.Height + 30;
        PdfGridLayoutResult gridResult = pdfGrid.Draw(page, new PointF(10, tableY));
        pdfGrid.Draw(page, new PointF(10, tableY));

        float nextY = gridResult.Bounds.Bottom + 10;
        graphics.DrawString($"Total amount : {totalAmount}", font, PdfBrushes.Black, new PointF(10, nextY));
        nextY += 15;
        graphics.DrawString($"Username : {username}", font, PdfBrushes.Black, new PointF(10, nextY));
        nextY += 15;
        graphics.DrawString($"Creation date: {DateTime.Now:MMMM dd, yyyy HH:mm:ss}", font, PdfBrushes.Black, new PointF(10, nextY));

        MemoryStream stream = new MemoryStream();
        doc.Save(stream);
        stream.Position = 0;
        doc.Close(true);

        return stream;
    }
}
