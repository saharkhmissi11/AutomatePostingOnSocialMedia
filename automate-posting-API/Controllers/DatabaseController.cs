using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostingOnSociallMedia.Models;
using PostingOnSociallMedia.sf_sezane;
using System.Web;
using Microsoft.EntityFrameworkCore;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Security.Cryptography.Xml;

namespace PostingOnSociallMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly SfSezaneContext _context;


        public DatabaseController(SfSezaneContext context)
        {
            _context = context;
        }
        [HttpGet("getMediaIdByUrl/{url}")]
        public async Task<ActionResult<int>> getIdByUrl(string url)
        {
            string encodedUrl = HttpUtility.UrlDecode(url);
            return _context.Medias.FirstOrDefault(m => m.Url == encodedUrl).Id;
        }
        [HttpGet("getProductIdByReference/{reference}")]
        public async Task<ActionResult<int>> getProductIdByReference(string reference)
        {
            return _context.Products.FirstOrDefault(p => p.Reference == reference).Id;

        }
        [HttpGet("getProductReferenceBy/{id}")]
        public async Task<ActionResult<string>> getProductIdByReference(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id).Reference;

        }
        [HttpGet("getProjectIdByTitle/{title}")]
        public async Task<ActionResult<int>> getProjectIdByTitle(string title)
        {
            return _context.Projects.FirstOrDefault(p => p.Title == title).Id;
        }
       [HttpGet("getVisibleProducts/{mediaId}/{projectId}")]
        public async Task<ActionResult> GetVisibleProducts(int mediaId,int projectId)
        {
            var productIds = _context.Visibleproducts
                   .Where(vp => vp.MediaId == mediaId && vp.ProjectId == projectId)
                   .Select(vp => vp.ProductId)
                   .ToList();
            string visibleProductsReference="";
            foreach (int productId in productIds)
            {
                var reference= _context.Products.FirstOrDefault(p => p.Id == productId).Reference;
                visibleProductsReference= visibleProductsReference+reference + ";";
            }
                return Ok(visibleProductsReference); 
        }
       
    }
}
