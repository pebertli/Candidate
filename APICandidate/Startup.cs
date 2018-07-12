using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICandidate.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APICandidate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Use SQL Database if in Azure, otherwise, use SQLite
            string conn = Configuration.GetConnectionString("CandidateDbConnection");


            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            services.AddDbContext<DBContext>(options =>
                        options.UseSqlServer(conn));
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(conn))
            //    {
            //        Console.WriteLine("\nQuery data example:");
            //        Console.WriteLine("=========================================\n");

            //        connection.Open();
            //        StringBuilder sb = new StringBuilder();
            //        sb.Append("SELECT * ");
            //        sb.Append("FROM Profile; ");
            //        String sql = sb.ToString();

            //        using (SqlCommand command = new SqlCommand(sql, connection))
            //        {
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    // Console.WriteLine("{0} {1}", reader.GetString(1), reader.GetString(2));
            //                    string s = reader.GetString(1);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (SqlException e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
            //Console.ReadLine();


            //else
            //    services.AddDbContext<DBContext>(options =>
            //            options.UseSqlite("Data Source=localdatabase.db"));

            // Automatically perform database migration
            //services.BuildServiceProvider().GetService<DBContext>().Database.Migrate();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
