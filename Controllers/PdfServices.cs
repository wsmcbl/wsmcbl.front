using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;


namespace wsmcbl.front.Controllers;

public class PdfServices : Controller
{
    public IActionResult Facturar()
    {
        return new ViewAsPdf();
    }
}