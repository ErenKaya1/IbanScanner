using System;
using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class IbanScannerContext : IdentityDbContext<IbanScannerUser, IbanScannerRole, Guid>
    {
        public IbanScannerContext(DbContextOptions<IbanScannerContext> options) : base(options)
        {

        }
    }
}