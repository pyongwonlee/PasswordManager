using PasswordManager.Models.Entities;
using PasswordManager.Models.Export;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IPasswordHelperRepository : IDisposable
    {
        IEnumerable<Password> Passwords { get; }

        IEnumerable<ExportAccountModel> PasswordsForExport { get; }

        IEnumerable PasswordListForExport { get; }

        IEnumerable<Password> GetPasswords(string category, string searchTerm);

        Password Find(int id);
    }
}