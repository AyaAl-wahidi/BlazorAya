using BlazorAya.Shared;
using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;

namespace BlazorAya.Server
{
    public class PatientContext : MsSqlFluentRepository<Patients>
    {
        public PatientContext(string connectionString) : base(new SqlConnection(connectionString)) { }
    }
}