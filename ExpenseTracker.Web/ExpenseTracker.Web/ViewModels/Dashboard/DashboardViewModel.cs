using ExpenseTracker.Web.ViewModels.Transfer;

namespace ExpenseTracker.Web.ViewModels.Dashboard;

public sealed class DashboardViewModel
{
    public decimal Balance { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public List<SplineChart> SplineChartData { get; set; }
    public List<DoughnutChart> DoughnutChartData { get; set; }
    public List<TransferViewModel> RecentTransfers { get; set; }
}

public sealed class SplineChart
{
    public string Day { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }

    public SplineChart(string day, decimal income, decimal expense)
    {
        Day = day;
        Income = income;
        Expense = expense;
    }

    public SplineChart()
    {

    }
}

public sealed class DoughnutChart
{
    public decimal Amount { get; set; }
    public string CategoryName { get; set; }
    public string FormattedAmount { get; set; }

    public DoughnutChart(decimal amount, string categoryName, string formattedAmount)
    {
        Amount = amount;
        CategoryName = categoryName;
        FormattedAmount = formattedAmount;
    }

    public DoughnutChart()
    {

    }
}
