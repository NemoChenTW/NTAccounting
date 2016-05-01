using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            ApplicationUser user0, user1;
            if (context.Users.Any(u => u.UserName == "nemo@mail.com"))
                user0 = context.Users.First(u => u.UserName == "nemo@mail.com");
            else
            {
                user0 = new ApplicationUser { UserName = "nemo@mail.com", Email = "nemo@mail.com" };
                var result = userManager.CreateAsync(user0, "Abc123").Result;
            }
            if (context.Users.Any(u => u.UserName == "teresa@mail.com"))
                user1 = context.Users.First(u => u.UserName == "teresa@mail.com");
            else
            {
                user1 = new ApplicationUser { UserName = "teresa@mail.com", Email = "teresa@mail.com" };
                var result = userManager.CreateAsync(user1, "Abc123").Result;
            }

            // 使用者群組
            UserGroup group0, group1, group2;
            context.UserGroup.AddRange(
                    group0 = new UserGroup
                    {
                        Name = "Nemo"
                    },
                    group1 = new UserGroup
                    {
                        Name = "Teresa"
                    },
                    group2 = new UserGroup
                    {
                        Name = "SweetHome"
                    }
                );

            // 使用者群組與使用者關係
            context.UserGroupApplicationUser.AddRange(
                    // Group Nemo
                    new UserGroupApplicationUser
                    {
                        UserGroupID = 0,
                        UserGroup = group0,
                        ApplicationUserID = user0.Id,
                        ApplicationUser = user0
                    },
                    // Group Teresa
                    new UserGroupApplicationUser
                    {
                        UserGroupID = 1,
                        UserGroup = group1,
                        ApplicationUserID = user1.Id,
                        ApplicationUser = user1
                    },

                    // Group SweetHome
                    new UserGroupApplicationUser
                    {
                        UserGroupID = 2,
                        UserGroup = group2,
                        ApplicationUserID = user0.Id,
                        ApplicationUser = user0
                    },
                    // Group SweetHome
                    new UserGroupApplicationUser
                    {
                        UserGroupID = 2,
                        UserGroup = group2,
                        ApplicationUserID = user1.Id,
                        ApplicationUser = user1
                    }
                );

            // 帳戶類型
            FinancialAccountType FinancialAccountType0, FinancialAccountType1;
            context.FinancialAccountType.AddRange(
                    FinancialAccountType0 = new FinancialAccountType
                    {
                        Type = "現金"
                    },
                    FinancialAccountType1 = new FinancialAccountType
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
                        TypeID = FinancialAccountType0.ID,
                        FinancialAccountType = FinancialAccountType0,
                        UserGroupID = group0.ID,
                        UserGroup = group0
                    },
                    FinancialAccount1 = new FinancialAccount
                    {
                        Name = "國泰世華",
                        Amount = 12000,
                        TypeID = FinancialAccountType0.ID,
                        FinancialAccountType = FinancialAccountType0,
                        UserGroupID = group0.ID,
                        UserGroup = group0
                    },
                    FinancialAccount2 = new FinancialAccount
                    {
                        Name = "身上現金",
                        Amount = 3000,
                        TypeID = FinancialAccountType1.ID,
                        FinancialAccountType = FinancialAccountType1,
                        UserGroupID = group0.ID,
                        UserGroup = group0
                    }
                );

            // 交易主類別
            MainTransactionCategory MainTransactionCategory0, MainTransactionCategory1, MainTransactionCategory2;
            context.MainTransactionCategory.AddRange(
                    MainTransactionCategory0 = new MainTransactionCategory
                    {
                        Name = "食物"
                    },
                    MainTransactionCategory1 = new MainTransactionCategory
                    {
                        Name = "民生設施"
                    },
                    MainTransactionCategory2 = new MainTransactionCategory
                    {
                        Name = "文藝"
                    }
                );

            // 交易子類別
            context.SubTransactionCategory.AddRange(
                    new SubTransactionCategory
                    {
                        Name = "早餐",
                        MainCategoryID = MainTransactionCategory0.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "午餐",
                        MainCategoryID = MainTransactionCategory0.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "水費",
                        MainCategoryID = MainTransactionCategory1.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "電費",
                        MainCategoryID = MainTransactionCategory1.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "瓦斯費",
                        MainCategoryID = MainTransactionCategory1.ID
                    },
                    new SubTransactionCategory
                    {
                        Name = "音樂會",
                        MainCategoryID = MainTransactionCategory2.ID
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
