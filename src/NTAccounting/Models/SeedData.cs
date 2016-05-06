using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NTAccounting.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (context.Database == null)
            {
                throw new Exception("DB is null");
            }

            if (isDBSeeded(context))
            {
                return;   // DB has been seeded
            }


            // 使用者
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            ApplicationUser userNemo, userTeresa;
            if (context.Users.Any(u => u.UserName == "nemo@mail.com"))
                userNemo = context.Users.First(u => u.UserName == "nemo@mail.com");
            else
            {
                userNemo = new ApplicationUser { UserName = "nemo@mail.com", Email = "nemo@mail.com" };
                userNemo.NickName = "Nemo";
                var result = userManager.CreateAsync(userNemo, "Abc123").Result;
            }
            if (context.Users.Any(u => u.UserName == "teresa@mail.com"))
                userTeresa = context.Users.First(u => u.UserName == "teresa@mail.com");
            else
            {
                userTeresa = new ApplicationUser { UserName = "teresa@mail.com", Email = "teresa@mail.com" };
                userTeresa.NickName = "Teresa";
                var result = userManager.CreateAsync(userTeresa, "Abc123").Result;
            }

            userNemo    = context.Users.Single(u => u.UserName == "nemo@mail.com");
            userTeresa  = context.Users.Single(u => u.UserName == "teresa@mail.com");

            // 使用者群組
            context.UserGroup.AddRange(
                    new UserGroup
                    {
                        Name = "Nemo"
                    },
                    new UserGroup
                    {
                        Name = "Teresa"
                    },
                    new UserGroup
                    {
                        Name = "SweetHome"
                    }
                );
            context.SaveChanges();
            UserGroup groupNemo         = context.UserGroup.Single(grp => grp.Name == "Nemo");
            UserGroup groupTeresa       = context.UserGroup.Single(grp => grp.Name == "Teresa");
            UserGroup groupSweetHome    = context.UserGroup.Single(grp => grp.Name == "SweetHome");


            // 設定 使用者代表群組
            /*
            *   Nemo 代表群組為 SweetHome
            *   Teresa 代表群組為 Teresa
            */
            userNemo.RepresentativeGrpupID      = groupSweetHome.ID;
            userTeresa.RepresentativeGrpupID    = groupTeresa.ID;

            var res = userManager.UpdateAsync(userNemo).Result;
            res = userManager.UpdateAsync(userTeresa).Result;



            // 使用者群組與使用者關係
            UserGroupApplicationUser relation0, relation1, relation2, relation3;
            context.UserGroupApplicationUser.AddRange(
                    // Group Nemo
                    relation0 = new UserGroupApplicationUser
                    {
                        UserGroupID = groupNemo.ID,
                        UserGroup = groupNemo,
                        ApplicationUserID = userNemo.Id,
                        ApplicationUser = userNemo
                    },
                    // Group Teresa
                    relation1 = new UserGroupApplicationUser
                    {
                        UserGroupID = groupTeresa.ID,
                        UserGroup = groupTeresa,
                        ApplicationUserID = userTeresa.Id,
                        ApplicationUser = userTeresa
                    },

                    // Group SweetHome
                    relation2 = new UserGroupApplicationUser
                    {
                        UserGroupID = groupSweetHome.ID,
                        UserGroup = groupSweetHome,
                        ApplicationUserID = userNemo.Id,
                        ApplicationUser = userNemo
                    },
                    // Group SweetHome
                    relation3 = new UserGroupApplicationUser
                    {
                        UserGroupID = groupSweetHome.ID,
                        UserGroup = groupSweetHome,
                        ApplicationUserID = userTeresa.Id,
                        ApplicationUser = userTeresa
                    }
                );
            //context.SaveChanges();

            userNemo.UserGroupApplicationUser.Add(relation0);
            userNemo.UserGroupApplicationUser.Add(relation2);

            var nemo = userManager.Users.Single(u => u.UserName == "nemo@mail.com");
            nemo.UserGroupApplicationUser.Add(relation0);
            nemo.UserGroupApplicationUser.Add(relation2);

            userTeresa.UserGroupApplicationUser.Add(relation1);
            userTeresa.UserGroupApplicationUser.Add(relation3);

            // 設定User0 的使用者與使用者群組關係
            //var userNemoRelation = from relation in context.UserGroupApplicationUser
            //                       where relation.ApplicationUserID == userNemo.Id
            //                       select relation;
            //userNemo.UserGroupApplicationUser.AddRange(userNemoRelation.ToList());
            //userNemo.UserGroupApplicationUser.Add(relation0);
            //userNemo.UserGroupApplicationUser.Add(relation2);

            //// 設定User1 的使用者與使用者群組關係
            //var userTeresaRelation = from relation in context.UserGroupApplicationUser
            //                         where relation.ApplicationUserID == userTeresa.Id
            //                         select relation;
            //userNemo.UserGroupApplicationUser.AddRange(userTeresaRelation.ToList());
            //userTeresa.UserGroupApplicationUser.Add(relation1);
            //userTeresa.UserGroupApplicationUser.Add(relation3);

            //userManager.UpdateAsync(userNemo);
            //userManager.UpdateAsync(userTeresa);


            // 帳戶類型
            FinancialAccountType AccountTypeCash;
            FinancialAccountType AccountTypeFreeMoney;
            context.FinancialAccountType.AddRange(
                    AccountTypeCash = new FinancialAccountType
                    {
                        Type = "現金"
                    },
                    AccountTypeFreeMoney = new FinancialAccountType
                    {
                        Type = "自由存款"
                    }
                );

            // 帳戶
            FinancialAccount FinancialAccount0, FinancialAccount1, FinancialAccount2;
            context.FinancialAccount.AddRange(
                    FinancialAccount0 = new FinancialAccount
                    {
                        Name = "富邦",
                        Amount = 80000,
                        TypeID = AccountTypeCash.ID,
                        FinancialAccountType = AccountTypeCash,
                        UserGroupID = groupNemo.ID,
                        UserGroup = groupNemo
                    },
                    FinancialAccount1 = new FinancialAccount
                    {
                        Name = "國泰世華",
                        Amount = 12000,
                        TypeID = AccountTypeCash.ID,
                        FinancialAccountType = AccountTypeCash,
                        UserGroupID = groupNemo.ID,
                        UserGroup = groupNemo
                    },
                    FinancialAccount2 = new FinancialAccount
                    {
                        Name = "身上現金",
                        Amount = 3000,
                        TypeID = AccountTypeFreeMoney.ID,
                        FinancialAccountType = AccountTypeFreeMoney,
                        UserGroupID = groupNemo.ID,
                        UserGroup = groupNemo
                    }
                );


            // 交易主類別
            MainTransactionCategory MainCategoryFood, MainCategorySystem, MainCategoryArt;
            context.MainTransactionCategory.AddRange(
                    MainCategoryFood = new MainTransactionCategory
                    {
                        Name = "食物"
                    },
                    MainCategorySystem = new MainTransactionCategory
                    {
                        Name = "民生設施"
                    },
                    MainCategoryArt = new MainTransactionCategory
                    {
                        Name = "文藝"
                    }
                );


            // 交易子類別
            context.SubTransactionCategory.AddRange(
                    new SubTransactionCategory
                    {
                        Name = "早餐",
                        MainCategoryID = MainCategoryFood.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "午餐",
                        MainCategoryID = MainCategoryFood.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "水費",
                        MainCategoryID = MainCategorySystem.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "電費",
                        MainCategoryID = MainCategorySystem.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "瓦斯費",
                        MainCategoryID = MainCategorySystem.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "音樂會",
                        MainCategoryID = MainCategoryArt.ID
                    }
                );
            context.SaveChanges();
        }

        private static bool isDBSeeded(ApplicationDbContext context)
        {
            if (isFinancialAccountSeeded(context)
                || isFinancialAccountTypeSeeded(context)
                || isMainTransactionCategorySeeded(context)
                || isSubTransactionCategorySeeded(context)
                || isTransactionSeeded(context)
                || isUserGroupSeeded(context)
                || isUserGroupApplicationUserpSeeded(context))
            {
                return true;
            }
            else
                return false;
        }

        // 檢查 FinancialAccount 是否已經有資料
        private static bool isFinancialAccountSeeded(ApplicationDbContext context)
        {
            if (context.FinancialAccount.Any())
                return true;
            else
                return false;
        }
        // 檢查 FinancialAccountType 是否已經有資料
        private static bool isFinancialAccountTypeSeeded(ApplicationDbContext context)
        {
            if (context.FinancialAccountType.Any())
                return true;
            else
                return false;
        }
        // 檢查 MainTransactionCategory 是否已經有資料
        private static bool isMainTransactionCategorySeeded(ApplicationDbContext context)
        {
            if (context.MainTransactionCategory.Any())
                return true;
            else
                return false;
        }
        // 檢查 SubTransactionCategory 是否已經有資料
        private static bool isSubTransactionCategorySeeded(ApplicationDbContext context)
        {
            if (context.SubTransactionCategory.Any())
                return true;
            else
                return false;
        }
        // 檢查 Transaction 是否已經有資料
        private static bool isTransactionSeeded(ApplicationDbContext context)
        {
            if (context.Transaction.Any())
                return true;
            else
                return false;
        }
        // 檢查 UserGroup 是否已經有資料
        private static bool isUserGroupSeeded(ApplicationDbContext context)
        {
            if (context.UserGroup.Any())
                return true;
            else
                return false;
        }
        // 檢查 UserGroupApplicationUser 是否已經有資料
        private static bool isUserGroupApplicationUserpSeeded(ApplicationDbContext context)
        {
            if (context.UserGroupApplicationUser.Any())
                return true;
            else
                return false;
        }
    }
}
