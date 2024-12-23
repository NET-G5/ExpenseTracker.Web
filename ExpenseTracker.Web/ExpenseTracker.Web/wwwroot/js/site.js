// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.e-menu-item').forEach(link => {
        const linkText = link.textContent.toLowerCase();
        const currentPath = location.pathname.toLowerCase();
        if (linkText === 'dashboard' && currentPath === '/') {
            link.classList.add('active-menu-item');
            console.log('Dashboard active:', link);
        }
        else if (currentPath.endsWith(linkText)) {
            link.classList.add('active-menu-item');
            console.log(link);
        }
        else {
            link.classList.remove('active-menu-item');
        }
    });
});