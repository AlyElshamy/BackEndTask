using AdminStaff.Data;
using AdminStaff.DTOs;
using AdminStaff.Entities;
using AdminStaff.Enums;
using AdminStaff.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace AdminStaff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public StudentController(IUnitOfWork unitOfWork, AppDbContext Context)
        {
            this.unitOfWork = unitOfWork;
            context = Context;
        }
        [HttpGet]
        public IActionResult Get() {
            var result = unitOfWork.Students.GetAll().ToList();
            return Ok(result.Select(a => new {
                a.ID,
                a.firstName
                , a.dateOfBirth,
                a.lasttName
            }));
        }
        [HttpPost]
        public IActionResult Post(StudentDTO student) {
            if (student == null) {
                return BadRequest();
            }
            var item = new Student() { firstName = student.firstName, lasttName = student.lasttName, dateOfBirth = student.dateOfBirth };
            context.Students.AddAsync(item);
            unitOfWork.Complete();
            return Ok(new StudentDTO { ID = item.ID, firstName = item.firstName, lasttName = item.lasttName, dateOfBirth = item.dateOfBirth });
        }
        [HttpPut]
        public IActionResult Update(StudentDTO student) {
            var item = context.Students.SingleOrDefault(a => a.ID == student.ID);
            if (item == null)
            {
                return BadRequest(student);
            }
            else
            {
                item.firstName = student.firstName;
                item.lasttName = student.lasttName;
                item.dateOfBirth = student.dateOfBirth;
                context.Students.Update(item);
                context.SaveChangesAsync();
                return Ok(new StudentDTO { ID = item.ID, firstName = item.firstName, lasttName = item.lasttName, dateOfBirth = item.dateOfBirth });
            }
        }
        //[Route("/api/items/{id}")]
        [HttpGet]
        [Route("{id}/GetNationalities")]
        public IActionResult GetNationalities(int id) {
            var item = context.Students.SingleOrDefault(a => a.ID == id);
            if (item == null)
            {
                return BadRequest(item);
            }
            else
            {
              
                int nationalityid = (int)item.nationality;
                context.Students.Update(item);
                context.SaveChangesAsync();
                return Ok(new StudentNationalitiesDTO { ID = item.ID, firstName = item.firstName, lasttName = item.lasttName, dateOfBirth = item.dateOfBirth ,Nationalityid= nationalityid });
            }
        }
        //[HttpPut]
        //[Route("{id}/Nationalities/{}")]
        //public IActionResult Nationalities(int id,int Id) {
        //    var item = context.Students.SingleOrDefault(a => a.ID == id);
        //    if (item == null)
        //    {
        //        return BadRequest(item);
        //    }
        //    else
        //    {

        //        int nationalityid = (int)item.nationality;
        //        context.Students.Update(item);
        //        context.SaveChangesAsync();
        //        return Ok(new StudentNationalitiesDTO { ID = item.ID, firstName = item.firstName, lasttName = item.lasttName, dateOfBirth = item.dateOfBirth ,Nationalityid= nationalityid });
        //    }
        //}

        //[Route("/api/items/{id}")]
        [HttpGet]
        [Route("{id}/Familymembers")]
        public IActionResult GetFamilymembers(int id)
        {
            var result = context.FamilyMembers.Where(a => a.studentId == id).ToList();
            if (result == null)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result.Select(a => new {id=a.ID,firstname=a.firstName,lastname=a.lasttName,relationid= (int)a.relatioship }));
            }
        }
        [HttpGet]
        [Route("{id}/FamilymembersNationality")]
        public IActionResult GetFamilymembersNationality(int id)
        {
            var result = context.FamilyMembers.Where(a => a.studentId == id).ToList();
            if (result == null)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result.Select(a => new { id = a.ID, firstname = a.firstName, lastname = a.lasttName, relationid = (int)a.relatioship,nationalityid=(int)a.nationality }));
            }
        }
        [HttpPut]
        [Route("{id}/Familymember")]
        public IActionResult GetFamilymember([FromBody]StudentDTO student,int id)
        {
            var result = context.FamilyMembers.Where(a => a.studentId == id).ToList();
            if (result == null)
            {
                return BadRequest(result);
            }
            else
            {
                context.FamilyMembers.Where(a => a.studentId == id).ExecuteUpdate(s => s.SetProperty(e => e.dateOfBirth, e => e.dateOfBirth.AddDays(1)));
                context.SaveChanges();
                return Ok(result.Select(a => new { id = a.ID, firstname = a.firstName, lastname = a.lasttName, relationid = (int)a.relatioship,nationalityid=(int)a.nationality }));
            }
        }

        [HttpDelete]
        [Route("Familymember/{id}")]
        public IActionResult DeleteFamilymember( int id)
        {
            var result = context.FamilyMembers.Find(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            else
            {
                context.FamilyMembers.Remove(result);
                context.SaveChanges();
                return Ok("Removed Succesfully");
            }
        }
        [HttpGet]
        [Route("Nationality")]
        public IActionResult GetNationalities()
        {
            var result = Enum.GetValues(typeof(NationalityEnum));
            var stringlist =new List<string>();
            if (result == null) return BadRequest(result);

            else { 
                foreach (var item in result)
                {
                    stringlist.Add(Enum.GetName(typeof(NationalityEnum), item));
                }
            return Ok(stringlist);
            }
            }
        }
    }

