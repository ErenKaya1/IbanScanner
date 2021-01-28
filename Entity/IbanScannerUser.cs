using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class IbanScannerUser : IdentityUser<Guid>
    {
        [NotMapped]
        public override string UserName { get; set; }

        [NotMapped]
        public override int AccessFailedCount { get; set; }

        [NotMapped]
        public override bool LockoutEnabled { get; set; }

        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [NotMapped]
        public override string PhoneNumber { get; set; }

        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }

        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }

        [StringLength(100)]
        public override string Email { get; set; }

        [StringLength(100)]
        public override string NormalizedEmail { get; set; }
    }
}