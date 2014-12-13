using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Export;
using System.Collections;

namespace PasswordManager.Models.Data
{
    public class EFPasswordHelperRepository : IPasswordHelperRepository
    {
        PasswordContext context;

        public EFPasswordHelperRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Password> Passwords
        {
            get 
            { 
                return context.Passwords
                    .Include(pw => pw.Company)
                    .Include(pw => pw.Company.Category)
                    .OrderBy(pw => pw.Company.Name)
                    .ToList();
            }
        }

        public IEnumerable<ExportAccountModel> PasswordsForExport 
        {
            get
            {
                return context.Passwords
                    .Include(pw => pw.Company)
                    .OrderBy(pw => pw.Company.Name)
                    .Select(pw => new ExportAccountModel
                    {
                        Company = pw.Company.Name,
                        UserName = pw.UserName,
                        Password = pw.PasswordCode,
                        Comment = pw.Comment ?? "",
                        Note1 = pw.Note1 ?? "",
                        Note2 = pw.Note2 ?? "",
                        Note3 = pw.Note3 ?? "",
                        Note4 = pw.Note4 ?? "",
                        Note5 = pw.Note5 ?? ""
                    })
                    .ToList();
            }
        }

        public IEnumerable PasswordListForExport
        {
            get
            {
                return PasswordsForExport;
            }
        }

        public IEnumerable<Password> GetPasswords(string category, string searchTerm)
        {
            return context.Passwords
                .Include(pw => pw.Company)
                .Include(pw => pw.Company.Category)
                .OrderBy(pw => pw.Company.Name)
                .Where(pw => ((string.IsNullOrEmpty(category) || pw.Company.Category.Name == category) &&
                              (string.IsNullOrEmpty(searchTerm) || pw.Company.Name.ToLower().Contains(searchTerm.ToLower()))))
                .ToList();
        }

        public Password Find(int id)
        {
            return context.Passwords
                    .Include(pw => pw.Company)
                    .Include(pw => pw.Company.Category)
                    .Where(pw => pw.PasswordId == id)
                    .SingleOrDefault();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}