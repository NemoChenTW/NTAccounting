using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace NTAccounting.Migrations
{
    public partial class AddTransactionTargetFinancialAccountID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_FinancialAccount_FinancialAccountType_TypeID", table: "FinancialAccount");
            migrationBuilder.DropForeignKey(name: "FK_FinancialAccount_UserGroup_UserGroupID", table: "FinancialAccount");
            migrationBuilder.DropForeignKey(name: "FK_SubTransactionCategory_MainTransactionCategory_MainCategoryID", table: "SubTransactionCategory");
            migrationBuilder.DropForeignKey(name: "FK_Transaction_FinancialAccount_FinancialAccountID", table: "Transaction");
            migrationBuilder.DropForeignKey(name: "FK_Transaction_SubTransactionCategory_SubTransactionCategoryID", table: "Transaction");
            migrationBuilder.DropForeignKey(name: "FK_UserGroupApplicationUser_ApplicationUser_ApplicationUserID", table: "UserGroupApplicationUser");
            migrationBuilder.DropForeignKey(name: "FK_UserGroupApplicationUser_UserGroup_UserGroupID", table: "UserGroupApplicationUser");
            migrationBuilder.AddColumn<int>(
                name: "TargetFinancialAccountID",
                table: "Transaction",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAccount_FinancialAccountType_TypeID",
                table: "FinancialAccount",
                column: "TypeID",
                principalTable: "FinancialAccountType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAccount_UserGroup_UserGroupID",
                table: "FinancialAccount",
                column: "UserGroupID",
                principalTable: "UserGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SubTransactionCategory_MainTransactionCategory_MainCategoryID",
                table: "SubTransactionCategory",
                column: "MainCategoryID",
                principalTable: "MainTransactionCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_FinancialAccount_FinancialAccountID",
                table: "Transaction",
                column: "FinancialAccountID",
                principalTable: "FinancialAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_SubTransactionCategory_SubTransactionCategoryID",
                table: "Transaction",
                column: "SubTransactionCategoryID",
                principalTable: "SubTransactionCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_FinancialAccount_TargetFinancialAccountID",
                table: "Transaction",
                column: "TargetFinancialAccountID",
                principalTable: "FinancialAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupApplicationUser_ApplicationUser_ApplicationUserID",
                table: "UserGroupApplicationUser",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupApplicationUser_UserGroup_UserGroupID",
                table: "UserGroupApplicationUser",
                column: "UserGroupID",
                principalTable: "UserGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_FinancialAccount_FinancialAccountType_TypeID", table: "FinancialAccount");
            migrationBuilder.DropForeignKey(name: "FK_FinancialAccount_UserGroup_UserGroupID", table: "FinancialAccount");
            migrationBuilder.DropForeignKey(name: "FK_SubTransactionCategory_MainTransactionCategory_MainCategoryID", table: "SubTransactionCategory");
            migrationBuilder.DropForeignKey(name: "FK_Transaction_FinancialAccount_FinancialAccountID", table: "Transaction");
            migrationBuilder.DropForeignKey(name: "FK_Transaction_SubTransactionCategory_SubTransactionCategoryID", table: "Transaction");
            migrationBuilder.DropForeignKey(name: "FK_Transaction_FinancialAccount_TargetFinancialAccountID", table: "Transaction");
            migrationBuilder.DropForeignKey(name: "FK_UserGroupApplicationUser_ApplicationUser_ApplicationUserID", table: "UserGroupApplicationUser");
            migrationBuilder.DropForeignKey(name: "FK_UserGroupApplicationUser_UserGroup_UserGroupID", table: "UserGroupApplicationUser");
            migrationBuilder.DropColumn(name: "TargetFinancialAccountID", table: "Transaction");
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAccount_FinancialAccountType_TypeID",
                table: "FinancialAccount",
                column: "TypeID",
                principalTable: "FinancialAccountType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAccount_UserGroup_UserGroupID",
                table: "FinancialAccount",
                column: "UserGroupID",
                principalTable: "UserGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SubTransactionCategory_MainTransactionCategory_MainCategoryID",
                table: "SubTransactionCategory",
                column: "MainCategoryID",
                principalTable: "MainTransactionCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_FinancialAccount_FinancialAccountID",
                table: "Transaction",
                column: "FinancialAccountID",
                principalTable: "FinancialAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_SubTransactionCategory_SubTransactionCategoryID",
                table: "Transaction",
                column: "SubTransactionCategoryID",
                principalTable: "SubTransactionCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupApplicationUser_ApplicationUser_ApplicationUserID",
                table: "UserGroupApplicationUser",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupApplicationUser_UserGroup_UserGroupID",
                table: "UserGroupApplicationUser",
                column: "UserGroupID",
                principalTable: "UserGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
