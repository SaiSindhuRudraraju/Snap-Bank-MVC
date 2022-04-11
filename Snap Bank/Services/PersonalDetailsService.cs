using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snap_Bank.Services
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        SnapDbContext snapDbContext;
        Mapper.Mapper map;
        PersonalDetails personalDetail;

        public PersonalDetailsService()
        {
            map = new Mapper.Mapper();
            personalDetail = new PersonalDetails();
            snapDbContext = new SnapDbContext();
        }

        public IEnumerable<PersonalDetails> Get()
        {
            return snapDbContext.personalDetails.ToList();
        }

        public bool Save(RegisterViewModel registerViewModel)
        {
            personalDetail = map.MapRegisterViewModelToPersonalDetails(registerViewModel, personalDetail);
            using (var dbContext = new SnapDbContext())
            {
                dbContext.personalDetails.Add(personalDetail);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Delete(int id)
        {
            using (var ent = new SnapDbContext())
            {
                //var user = ent.personalDetails.Where(s => s.UserId == id).FirstOrDefault();
                //if (user != null)
                //{
                //    ent.personalDetails.Remove(user);
                //    ent.SaveChanges();
                //    return true;
                //}
                return false;
            }
        }
        public bool Put(RegisterViewModel registerViewModel)
        {
            //using (var ent = new SnapDbContext())
            //{
            //    var temp = ent.personalDetails.Find(personalDetail.UserId);
            //    if (temp != null)
            //    {
            //        temp.FirstName = personalDetail.FirstName;
            //        temp.LastName = personalDetail.LastName;
            //        temp.DateOfBirth = personalDetail.DateOfBirth;
            //        temp.Gender = personalDetail.Gender;
            //        temp.Gmail = personalDetail.Gmail;
            //        temp.MobileNumber = personalDetail.MobileNumber;
            //    }
            //    ent.SaveChanges();
            //    return true;
            //}
            return false;
        }
    }
}