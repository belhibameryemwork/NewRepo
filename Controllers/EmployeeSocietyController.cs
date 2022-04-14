using Employe.Data;
using Employe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSocietyController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IWebHostEnvironment env;
        public EmployeeSocietyController(DataContext context, IWebHostEnvironment env)
        {

            this.dataContext = context;
            this.env = env;
        }



        [HttpGet]
        public async Task<JsonResult> Get()
        {


            return new JsonResult(await this.dataContext.EmployeSociete.ToListAsync());

        }



        [HttpPost]
        public async Task<JsonResult> Post(EmployeSociete eso)
        {


            this.dataContext.EmployeSociete.Add(eso);
            await this.dataContext.SaveChangesAsync();
            return new JsonResult("success");
        }




        [HttpPut]
        public async Task<JsonResult> Put(EmployeSociete eso)
        {
            var EmployeSociety = await this.dataContext.EmployeSociete.FindAsync(eso.EmployeSocietyId);
            if (EmployeSociety == null)
            {
                return new JsonResult("EmployeeSociety not found");
            }
            if (eso.Company != null)
            {
                EmployeSociety.Company = eso.Company;
            }
            if (eso.PhotoFileName != null)
            {
                EmployeSociety.PhotoFileName = eso.PhotoFileName;
            }
            if (eso.DateOfJoining != null)
            {
                EmployeSociety.DateOfJoining = eso.DateOfJoining;
            }
            if (eso.EmployeSocietyName != null)
            {
                EmployeSociety.EmployeSocietyName = eso.EmployeSocietyName;
            }
            
            await this.dataContext.SaveChangesAsync();

            return new JsonResult("ypdated");
        }



        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {


            var EmployeSociety = await this.dataContext.EmployeSociete.FindAsync(id);
            if (EmployeSociety == null)
            {
                return new JsonResult("EmployeSociety not found");
            }

            dataContext.EmployeSociete.Remove(EmployeSociety);
            await this.dataContext.SaveChangesAsync();

            return new JsonResult("Deleted");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = this.env.ContentRootPath + "/Photos/" + filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception ex)
            {
                return new JsonResult("anonymous.png");
            }
        }

    }
}
