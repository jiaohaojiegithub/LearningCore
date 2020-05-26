
using LearningCore.Data;
using LearningCore.Data.MVCModels;
using LearningCore.Data.RazorModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace LearningCore.Service
{
    public class LearningCoreContext : DbContext
    {
        public LearningCoreContext()
        {
        }

        public LearningCoreContext(DbContextOptions<LearningCoreContext> options)
      : base(options)
        { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Base_Attribute> BaseAttributes { get; set; }
        public DbSet<Mx_Question> MxQuestions { get; set; }
        public DbSet<Mx_QuestionCategory> MxQuestionCategories { get; set; }
        public DbSet<Mx_Attribute> MxAttributes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<AppFile> Files { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlServer(
                    //@"Server=(localdb)\\mssqllocaldb;Database=Blogging;Trusted_Connection=True;"
                    @"Data Source=.;Initial Catalog=LearningCoreDb;Integrated Security=True"
                    , options => options.EnableRetryOnFailure());
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region 模型验证
            modelBuilder.Entity<Blog>(e =>
            {
                e.HasKey(b => b.Id);
                e.Property(b => b.Id).ValueGeneratedOnAdd();
                e.Property(b => b.Url).IsRequired().HasComment("说明");
                e.Property(b => b.IsDeleted).HasDefaultValue<bool>(false);//设置默认值
                e.Property(b => b.Name).HasComputedColumnSql("[FamilyName]+[LastName]");//关系型数据库计算其值
                e.Property(b => b.LastName).IsConcurrencyToken();//并发标记控制
                e.Property(b => b.Timestamp).IsRowVersion();//在每次插入或更新行时，数据库会自动为其生成新值.确认数据操作的并发唯一性
                //e.HasData(new Blog { FamilyName = "焦", LastName = "豪杰", Url = "http://baidu.com" });//数据种子
                e.Property(b => b.ModifiedTime).HasDefaultValueSql("GETDATE()");
                e.Property(b => b.CreatedTime).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Base_Attribute>(e =>
            {
                e.HasKey(b => b.Id);
                e.Property(b => b.Id).ValueGeneratedOnAdd();
                e.HasDiscriminator<string>("Discriminator")//鉴别器 HasDiscriminator(b=>b.Discriminator)
                 .HasValue<Base_Attribute>("Base")
                 .HasValue<Mx_Attribute>("Mx");
                //e.Property(b => b.AttributeName).HasColumnName("AttributeName");//共享列 列名相同则共享列
                e.Property(b => b.ModifiedTime).HasDefaultValueSql("GETDATE()");
                e.Property(b => b.CreatedTime).HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Mx_Attribute>(e =>
            {
                //e.Property(b => b.AttributeName).HasColumnName("AttributeName");//共享列 列名相同则共享列
            });
            modelBuilder.Entity<Mx_Question>(e =>
            {
                e.HasOne(b => b.Mx_QuestionCategory).WithMany(p => p.Mx_Questions)//一对一 对应单条类别  反向导航 类别对应多条题目
                 .HasForeignKey(b => b.Mx_QuestionCategoryId)//外键设置
                                                             //.IsRequired() //必选关系
                 .OnDelete(DeleteBehavior.ClientSetNull)//级联删除，对于可选关系设置 导航属性若删除 则外键设置为null
                 ;
                e.Property(b => b.QuestionType).HasConversion<string>();//内置枚举值转换
                e.Property(b => b.ModifiedTime).HasDefaultValueSql("GETDATE()");
                e.Property(b => b.CreatedTime).HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<Movie>(e =>
            {
                e.HasKey(b => b.Id);
                e.Property(b => b.Id).ValueGeneratedOnAdd();
                e.Property(b => b.Price).HasColumnType("decimal(18, 2)");
                e.Property(b => b.ModifiedTime).HasDefaultValueSql("GETDATE()");
                e.Property(b => b.CreatedTime).HasDefaultValueSql("GETDATE()");
            });
            //modelBuilder.Entity<TodoItem>(e =>
            //{
            //    e.HasKey(b => b.Id);
            //    e.Property(b => b.Id).ValueGeneratedOnAdd();
             
            //});
            #endregion
        }
    }
}
