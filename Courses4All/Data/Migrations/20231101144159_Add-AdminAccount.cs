using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;
using System.Xml.Linq;

namespace Courses4All.Data.Migrations
{
    public partial class AddAdminAccount : Migration
    {
        const string ADMINGUID = "d1771b08-ea93-4a4b-a9f9-cf2eb5e31ad8";
        const string ADMINROLE = "9cc79068-48be-4bfc-b42e-66f4f5cacb62";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(null, "Course4all");
            //Adding admin to ASP.User Table
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("INSERT INTO AspNetUsers(Id,UserName, NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,Address1,Address2,FirstName,LastName,PostCode) VALUES");
            stringBuilder.AppendLine($"('{ADMINGUID}', 'admin', 'admin', 'admin@Cours4all.com', 'admin@Cours4all.com', 0, '{hashedPassword}', '', '', '0763774151',0 ,0 , 0, 0, '113 clifton', '', 'Deshan', 'Maduranga', 'PL444GA');");
            migrationBuilder.Sql(stringBuilder.ToString());

            //Adding User Role to sql Table
            migrationBuilder.Sql($"INSERT INTO  AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp) VALUES ('{ADMINROLE}','Administrator','ADMIN','')");
            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles(UserId ,RoleId) VALUES ('{ADMINGUID}','{ADMINROLE}')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = {ADMINGUID}");
            migrationBuilder.Sql($"DELETE FROM AspNetRoles  WHERE Id = {ADMINROLE}");
            migrationBuilder.Sql($"DELETE FROM AspNetUserRoles  WHERE UserId = {ADMINGUID} AND RoleId = {ADMINROLE}");
        }
    }
}
