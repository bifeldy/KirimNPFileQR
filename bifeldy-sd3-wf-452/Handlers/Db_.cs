/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Mail         :: bias@indomaret.co.id
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Turunan `CDatabase`
 *              :: Harap Didaftarkan Ke DI Container
 *              :: Instance Semua Database Bridge
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

using bifeldy_sd3_lib_452.Abstractions;
using bifeldy_sd3_lib_452.Databases;
using bifeldy_sd3_lib_452.Models;

using KirimNPFileQR.Utilities;

namespace KirimNPFileQR.Handlers {

    public interface IDb : IDbHandler {
        Task<DataTable> OraPg_GetDataTable(string sqlQuery);
        Task<DateTime> OraPg_GetYesterdayDate(int lastDay);
        Task<DateTime> OraPg_GetCurrentTimestamp();
        Task<DateTime> OraPg_GetCurrentDate();
        Task<CDbExecProcResult> OraPg_CALL_(string procName);
        Task<string> GetURLWebService(string webType);
    }

    public sealed class CDb : CDbHandler, IDb {

        private readonly IApp _app;

        public CDb(IApp app, IOracle oracle, IPostgres postgres, IMsSQL mssql) : base(app, oracle, postgres, mssql) {
            _app = app;
        }

        public async Task<DataTable> OraPg_GetDataTable(string sqlQuery) {
            return await OraPg.GetDataTableAsync(sqlQuery);
        }

        public async Task<DateTime> OraPg_GetYesterdayDate(int lastDay) {
            return await OraPg.ExecScalarAsync<DateTime>(
                $@"
                    SELECT {(_app.IsUsingPostgres ? "CURRENT_DATE" : "TRUNC(SYSDATE)")} - :last_day
                    {(_app.IsUsingPostgres ? "" : "FROM DUAL")}
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "last_day", VALUE = lastDay }
                }
            );
        }

        public async Task<DateTime> OraPg_GetCurrentTimestamp() {
            return await OraPg.ExecScalarAsync<DateTime>($@"
                SELECT {(_app.IsUsingPostgres ? "CURRENT_TIMESTAMP" : "SYSDATE FROM DUAL")}
            ");
        }

        public async Task<DateTime> OraPg_GetCurrentDate() {
            return await OraPg.ExecScalarAsync<DateTime>($@"
                SELECT {(_app.IsUsingPostgres ? "CURRENT_DATE" : "TRUNC(SYSDATE) FROM DUAL")}
            ");
        }

        public async Task<CDbExecProcResult> OraPg_CALL_(string procedureName) {
            return await OraPg.ExecProcedureAsync(procedureName);
        }

        /* ** */

        public async Task<string> GetURLWebService(string webType) {
            return await OraPg.ExecScalarAsync<string>(
                $@"SELECT WEB_URL FROM DC_WEBSERVICE_T WHERE WEB_TYPE = :web_type",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "web_type", VALUE = webType }
                }
            );
        }

    }

}
