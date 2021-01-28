using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class IbanScannerUser : IdentityUser<Guid>
    {
        [Required]
        [StringLength(12)]
        [Column(TypeName = "varchar(12)")]
        public override string UserName { get; set; }

        [Required]
        [StringLength(12)]
        [Column(TypeName = "varchar(12)")]
        public override string NormalizedUserName { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public override string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public override string NormalizedEmail { get; set; }

        [Required]
        public override string PasswordHash { get; set; }

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
    }
}