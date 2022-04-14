using Employe.Data;
using Employe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeSecurityController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IWebHostEnvironment env;
        public EmployeSecurityController(DataContext context, IWebHostEnvironment env)
        {
          
          this.dataContext = context;
            this.env = env;
        }



        [HttpGet]
        public async Task<JsonResult> Get()
        {
           

            return new JsonResult(await this.dataContext.EmployeSecurity.ToListAsync());

        }



        [HttpPost]
        public async Task<JsonResult> Post(EmployeSecurity es)
        {
           
           
            this.dataContext.EmployeSecurity.Add(es);
            await this.dataContext.SaveChangesAsync();
            return new JsonResult("success");
        }




        [HttpPut]
        public async Task<JsonResult> Put(EmployeSecurity es)
        {
            var EmployeSecurity = await this.dataContext.EmployeSecurity.FindAsync(es.EmployeSecurityId);
            if (EmployeSecurity == null)
            {
                return new JsonResult("EmployeeSecurity not found");
            }
            
            if (es.Position !=null) {
                EmployeSecurity.Position = es.Position;
            }
            if (es.EmployeSecurityName != null)
            {
                EmployeSecurity.EmployeSecurityName = es.EmployeSecurityName;
            }
            if (es.DateOfJoining != null)
            {
                EmployeSecurity.DateOfJoining = es.DateOfJoining;
            }
            if (es.PhotoFileName != null)
            {
                EmployeSecurity.PhotoFileName = es.PhotoFileName;
            }



            await this.dataContext.SaveChangesAsync();

            return new JsonResult("ypdated");
        }



        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            

            var EmployeSecurity = await this.dataContext.EmployeSecurity.FindAsync(id);
            if (EmployeSecurity == null)
            {
                return new JsonResult("EmployeSecurity not found");
            }

            dataContext.EmployeSecurity.Remove(EmployeSecurity);
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

