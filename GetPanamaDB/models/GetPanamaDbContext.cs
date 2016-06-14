using Common.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetPanamaDB.models
{
    public class GetPanamaDbContext : DbContext
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“Model1”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“EFLibForApi.emms.Model1”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“Model1”
        //连接字符串。
        public GetPanamaDbContext()
            : base("name=PanamaDBConnection")
        {
            //this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Database.Log = message => logger.DebugFormat(message);//.Replace("\n", " ")
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<EntitysAll> EntitysAll { get; set; }
        public virtual DbSet<Connections> Connections { get; set; }
        public virtual DbSet<CountryItems> CountryItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
        public static GetPanamaDbContext Create()
        {
            return new GetPanamaDbContext();
        }
    }
}
