using ActualizadorDoctosUnigis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;

namespace ActualizadorDoctosUnigis
{
    public class Qrys
    {

        public static void InsertUpdateRows(string resource, string eventa, string account_name, int route, int count_id, string date, string truck, string truck_driver, bool started, DateTime stared_at, bool ended, DateTime ended_at)
        {
            int S = 0;
            int E = 0;
            if (started)
                S = 1;
            if (ended)
                E = 1;

            string fecha_inicio = stared_at.ToString();
            string fecha_inicioS = fecha_inicio.Substring(0, fecha_inicio.Length - 6);

            string fecha_fin = ended_at.ToString();
            string fecha_finS = fecha_fin.Substring(0, fecha_fin.Length - 6);


            using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4,9001 ; Initial Catalog = BMS; User ID = sa; Password = @Sistemas74"))
            {
                string saveStaff = "";

                if (fecha_finS is null && fecha_inicioS is null)
                {
                    saveStaff = "Insert into EYP_BEETRACK_ACT_RUTA values('" + resource + "','" + eventa + "','" + account_name + "'," + route + "," + count_id + ",convert(smalldatetime, '" + date + "',103),'" + truck + "','" + truck_driver + "'," + S + ",null," + E + ",null)";

                }
                else if (fecha_inicioS is null)
                {
                    saveStaff = "Insert into EYP_BEETRACK_ACT_RUTA values('" + resource + "','" + eventa + "','" + account_name + "'," + route + "," + count_id + ",convert(smalldatetime, '" + date + "',103),'" + truck + "','" + truck_driver + "'," + S + ",null," + E + ",convert(smalldatetime,'" + fecha_finS + "',103))";

                }
                else if (fecha_finS == null)
                {

                    saveStaff = "Insert into EYP_BEETRACK_ACT_RUTA values('" + resource + "','" + eventa + "','" + account_name + "'," + route + "," + count_id + ",convert(smalldatetime, '" + date + "',103),'" + truck + "','" + truck_driver + "'," + S + ",convert(smalldatetime,'" + fecha_inicioS + "',103)," + E + ",null)";

                }
                else
                {

                    saveStaff = "Insert into EYP_BEETRACK_ACT_RUTA values('" + resource + "','" + eventa + "','" + account_name + "'," + route + "," + count_id + ",convert(smalldatetime, '" + date + "',103),'" + truck + "','" + truck_driver + "'," + S + ",convert(smalldatetime,'" + fecha_inicioS + "',103)," + E + ",convert(smalldatetime,'" + fecha_finS + "',103))";
                }
                using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                {
                    querySaveStaff.Connection = openCon;

                    openCon.Open();

                    querySaveStaff.ExecuteNonQuery();
                }
            }




        }

        public DataTable selectitems()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("SELECT RTRIM(emb.abrev_trans) + '-' + rtrim(emb.folio) as 'N° DOCUMENTO', case emb.transaccion when '713' then fMin.fecha   when'35' then emb.fecha  else F.FECHA end AS FECHA_DOC,case emb.transaccion when '713' then  movin.fecha when '35' then emb.fecha else f.fecha_entrega end  as FechaEntrega,f.cod_cte,cl.razon_social,cl.tel1,cl.tel2,cl.e_mail, case when emb.peso_total <= 0 then 0.01*emb.cantidad  ");
            qry.Append(" else emb.peso_total* emb.cantidad end as Peso_KG, ");
            qry.Append("case(rtrim(case emb.transaccion when '713' then isnull(ISNULL(case d3.latitud when '0' then convert(varchar(MAX), d4.latitud) else convert(varchar(MAX), d3.latitud) end,convert(varchar(MAX), d4.latitud)),'0') ");
            qry.Append("else isnull(ISNULL(case dom.latitud when'0' then convert(varchar(MAX), d2.latitud) else convert(varchar(MAX), dom.latitud) end,convert(varchar(MAX), d2.latitud)),'0') end)) when '0.000000' then '0' else ");
            qry.Append(" (case emb.transaccion when '713' then isnull(ISNULL(case d3.latitud when '0' then convert(varchar(MAX), d4.latitud) else convert(varchar(MAX), d3.latitud) end,convert(varchar(MAX), d4.latitud)),'0') ");
            qry.Append("else isnull(ISNULL(case dom.latitud when'0' then convert(varchar(MAX), d2.latitud) else convert(varchar(MAX), dom.latitud) end,convert(varchar(MAX), d2.latitud)),'0') end) end as LATITUD, ");
            qry.Append("case rtrim(case emb.transaccion when '713' then  isnull(ISNULL(case d3.longitud when'0' then convert(varchar(MAX), d4.longitud) else convert(varchar(MAX), d3.longitud) end,convert(varchar(MAX), d4.longitud)),'0') ");
            qry.Append("else isnull(ISNULL(case dom.longitud when '0' then convert(varchar(max), d2.longitud) else convert(varchar(max), dom.longitud) end,convert(varchar(MAX), d2.longitud * -1)),'0') end) when '0.000000'then '0' else ");
            qry.Append("          (case emb.transaccion when '713' then isnull(ISNULL(case d3.longitud when'0' then convert(varchar(max), d4.longitud*-1) else convert(varchar(max), d3.longitud * -1) end,convert(varchar(max), d4.longitud * -1)),'0' ) ");
            qry.Append("else isnull(ISNULL(case dom.longitud when '0' then convert(varchar(max), d2.longitud * -1) else convert(varchar(max), dom.longitud * -1) end,convert(varchar(max), d2.longitud * -1)) ,'0') end) end as LONGITUD, ");
            qry.Append("case when emb.transaccion = '35' then RTRIM(estab.calle) +' ' + RTRIM(estab.num_exterior) + ', ' + RTRIM(estab.colonia) + ', ' + RTRIM(estab.cod_postal) + ' ' + RTRIM(estab.pobmunedo) ");
            qry.Append("when emb.transaccion = '713' then(select  ");
            qry.Append("case when(select(RTRIM(d3.calle) + ' ' + RTRIM(d3.num_exterior) + ', ' + RTRIM(d3.colonia) + ', ' + RTRIM(d3.cod_postal) + ' ' + RTRIM(d3.pobmunedo))) is not null then ");
            qry.Append(" (select(RTRIM(d3.calle) + ' ' + RTRIM(d3.num_exterior) + ', ' + RTRIM(d3.colonia) + ', ' + RTRIM(d3.cod_postal) + ' ' + RTRIM(d3.pobmunedo)))  else ");
            qry.Append("(select(select(RTRIM(calle) + ' ' + RTRIM(num_exterior) + ', ' + RTRIM(colonia) + ', ' + RTRIM(cod_postal) + ' ' + RTRIM(pobmunedo)) from clientes c where c.cod_cte = ft.cod_cte and c.cod_cte <> '0') from facremtick ft where mi.folio_referencia = ft.folio and mi.transaccion_referencia = ft.transaccion) end ");
            qry.Append(" from movimientos_internos(nolock) mi where transaccion = '713' and folio = emb.folio) ");
            qry.Append(" when dom.calle is null then(select RTRIM(calle) + ' ' + RTRIM(num_exterior) + ', ' + RTRIM(colonia) + ', ' + RTRIM(cod_postal) + ' ' + RTRIM(pobmunedo) from clientes (nolock)c where c.cod_cte = emb.cod_cte and c.cod_cte <> '0') ");
            qry.Append("else isnull((RTRIM(dom.calle) + ' ' + RTRIM(dom.num_exterior) + ', ' + RTRIM(dom.colonia) + ', ' + RTRIM(dom.cod_postal) + ' ' + RTRIM(dom.pobmunedo)), (RTRIM(d2.calle) + ' ' + RTRIM(d2.num_exterior) + ', ' + RTRIM(d2.colonia) + ', ' + RTRIM(d2.cod_postal) + ' ' + RTRIM(d2.pobmunedo))) end as 'DIRECCION', ");
            qry.Append("emb.desc_producto as 'NOMBRE ITEM',  emb.cant_pend  as 'CANTIDAD', emb.cod_prod as 'CODIGO ITEM', ");
            qry.Append("' ' + convert(varchar, getdate(), 103) as 'FECHA MIN ENTREGA', ' ' + convert(varchar, getdate(), 103) as 'FECHA MAX ENTREGA', ' 08:00' as 'MIN VENTANA HORARIA 1', ' 18:30' as 'MAX VENTANA HORARIA 1', ");
            qry.Append("' ' as 'MIN VENTANA HORARIA 2', ' ' as 'MAX VENTANA HORARIA 2', eys.precio_lista as 'COSTO ITEM', ");
            qry.Append("case when emb.peso_total <= 0 then 0.01 ");
            qry.Append(" else emb.peso_total end as 'CAPACIDAD UNO', ' ' as 'CAPACIDAD DOS', ");
            qry.Append(" ' 15' as 'SERVICE TIME', ' ' as 'IMPORTANCIA', ");
            qry.Append("case when emb.cod_cte = '0' then RTRIM(emb.cod_cte) +' - ' + emb.folio else ");
            qry.Append("   emb.cod_cte end as 'IDENTIFICADOR CONTACTO', ");
            qry.Append(" emb.razon_social as 'NOMBRE CONTACTO', ");
            qry.Append("case when emb.transaccion = '35' then((estab.telefono1)) ");
            qry.Append("  when emb.transaccion = '713' then(select  ");
            qry.Append("case when(select telefono from domicilios where folio = mi.folio_referencia and transaccion = mi.transaccion_referencia) is not null then ");
            qry.Append(" (select telefono from domicilios where folio = mi.folio_referencia and transaccion = mi.transaccion_referencia) else ");
            qry.Append("         (select(select c.tel1 from clientes c where c.cod_cte = ft.cod_cte) from facremtick ft where mi.folio_referencia = ft.folio and mi.transaccion_referencia = ft.transaccion) end ");
            qry.Append("from movimientos_internos mi where transaccion = '713' and folio = emb.folio) ");
            qry.Append(" when dom.telefono is null then(select c.tel1 from clientes c where c.cod_cte = emb.cod_cte) ");
            qry.Append(" else dom.telefono end as 'TELEFONO', ");
            qry.Append(" ' ' as 'EMAIL CONTACTO', estab.abreviatura as 'CT ORIGEN', ");
            qry.Append("case when emb.transaccion = '35' then(select(select(RTRIM(dom.referencia)) from establecimientos where cod_estab = movin.cod_estab_alterno) from movimientos_internos movin where movin.folio = emb.folio and movin.transaccion = emb.transaccion) ");
            qry.Append(" when emb.transaccion = '713' then(select  ");
            qry.Append("case when((RTRIM(dom.referencia))) is not null then ");
            qry.Append(" (select(RTRIM(dom.referencia))) END ");
            qry.Append("from movimientos_internos (nolock)mi where transaccion = '713' and folio = emb.folio) ");
            qry.Append("when dom.calle is null then(select RTRIM(c.calle) from clientes (nolock)c where c.cod_cte = emb.cod_cte and c.cod_cte <> '0') ");
            qry.Append("else dom.referencia end as 'REFERENCIA DOMICILIO' , ");
            qry.Append(" coalesce(dom.calle, d2.calle, d3.calle, d4.calle) as Calle ,coalesce(dom.num_exterior, d2.num_exterior, d3.num_exterior, d4.num_exterior) as NumeroPuerta,Coalesce(dom.colonia, d2.colonia, d3.colonia, d4.colonia) as colonia ,coalesce(dom.pobmunedo, d2.pobmunedo, d3.pobmunedo, d4.pobmunedo) as Barrio,coalesce(dom.poblacion, d2.poblacion, d3.poblacion, d4.poblacion) as Localidad,coalesce(dom.poblacion, d2.poblacion, d3.poblacion, d4.poblacion) as Partido,coalesce('', '') as Estado,coalesce('', '') as pais,cl.cod_cte as RefDomicilioExterno,coalesce(dom.nombre, d2.nombre, d3.nombre, d4.nombre) as DomicilioDescripcion ");
            qry.Append(" , '0630' as InicioHorario1, '0000' as InicioHorario2 ,'2359' as FinHorario1 ,'0000'FinHorario2 ,'15' as TiempoEspera ,coalesce(dom.cod_postal, d2.cod_postal, d3.cod_postal, d4.cod_postal) as DomCodPostal,coalesce(dom.Alias, d2.Alias, d3.Alias, d4.Alias) as Contacto, estab.cod_estab as CodigoSucursal, CONCAT(RTRIM(estab.abreviatura), rtrim(estab.cod_estab)) as RefDepositoExterno, estab.cod_postal as CodigoPostalEstab ");
            qry.Append("  , lp.nombre AS Linea,fam.nombre as Sublinea ,cl.razon_social as RazonSocialCL ");
            qry.Append(" FROM[dbo].[F1534_Embarques_unigis]('10') emb ");
            qry.Append(" left join entysal(nolock) eys on emb.folio = eys.folio and emb.transaccion = eys.transaccion and emb.cod_prod = eys.cod_prod ");
            qry.Append(" left join establecimientos estab on eys.cod_estab = estab.cod_estab ");
            qry.Append(" left join facremtick f on emb.folio = f.folio and emb.transaccion = f.transaccion ");
            qry.Append(" left join domicilios dom(nolock) on dom.folio = f.folio and dom.transaccion = f.transaccion ");
            qry.Append(" LEFT JOIN movimientos_internos movin ON movin.folio = emb.folio and movin.transaccion = emb.transaccion ");
            qry.Append(" left join domicilios d2 on d2.folio = f.folio_origen and d2.transaccion = f.transaccion_origen ");
            qry.Append(" LEFT join facremtick fMin on fMin.folio = movin.folio_referencia And fMin.transaccion = movin.transaccion_referencia ");
            qry.Append(" LEFT join domicilios d3 on d3.folio = fMin.folio And d3.transaccion = fMin.transaccion ");
            qry.Append(" LEFT join domicilios d4 on d4.folio = fMin.folio_origen And d4.transaccion = fMin.folio_origen ");
            qry.Append(" left join pedcte p on p.folio = f.folio_origen  AND p.transaccion = f.transaccion ");
            qry.Append(" left join clientes cl on cl.cod_cte = f.cod_cte ");
            qry.Append(" LEFT JOIN productos PR ON PR.cod_prod = EYS.cod_prod ");
            qry.Append(" LEFT JOIN familias FAM ON PR.familia = FAM.familia ");
            qry.Append(" LEFT JOIN lineas_productos LP ON LP.linea_producto = PR.linea_producto ");
            qry.Append("outer apply (select * from dbo.eyp_direccion(emb.folio,emb.transaccion))  as TDir");
            qry.Append(" where emb.nombre_ruta <> '99' ");
            qry.Append(" and ");
            qry.Append(" emb.domicilio not like '%. . C.P. 22253  Mexicali, BC%' ");
            // qry.Append(" And emb.folio='"+folio+"' and emb.transaccion='"+transaccion+"'  ");
            qry.Append(" And emb.fecha > DATEADD(MM, -6, GETDATE()) and emb.status='0'");
            qry.Append(" order by emb.transaccion ASC ");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());
            return dataTable;
        }


        public DataTable selectitems(string establecimiento)
        {
            DataTable dtaux = new DataTable();
            DataTable dataTablep = new DataTable();
            SqlCommand cmd2 = new SqlCommand();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDA2;
            Conexion cnn2 = new Conexion();
            cnn2.conexion();
            StringBuilder qry2 = new StringBuilder();
            qry2.Append("select es.servidor,es.bbdd,e.cod_estab,es.unigis  from Eyp_Servidores es ");
            qry2.Append("left join establecimientos e on es.Cod_estab=e.cod_estab where bbdd is not null ");
            qry2.Append("and e.nombre='" + establecimiento + "'");
            cmd2.CommandText = qry2.ToString();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandTimeout = 0;
            cmd2.Connection = cnn2.conexion();
            sqlDA2 = new SqlDataAdapter(cmd2);
            sqlDA2.Fill(dataTable2);
            cnn2.cerrarconexion(cnn2.conexion());
            string servidor = "";
            string bdd = "";
            string codestab = "";
            foreach (DataRow dr in dataTable2.Rows)
            {
                servidor = dr[0].ToString();
                bdd = dr[1].ToString();
                codestab = dr[2].ToString();
            }
            cnn2.cerrarconexion(cnn2.conexion());
            if (servidor == "192.168.8.4,9001")
            {
                bdd = "BMS";
            }


            //if (servidor.Trim().ToUpper() == "192.168.18.99\\BMS")
            //{
            //    servidor = "192.168.8.4,9001";
            //    bdd = "BMS03082022";
            //}


            //if (codestab.Trim() == "21")
            //{
            //    servidor = "192.168.8.4,9001";
            //    bdd = "BMS03082022";

            //}

            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

                cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("EXEC EYP_UNIGIS '"+codestab+"' ");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTableD);
            dtaux.Merge(dataTableD, true);

            


            //llenar con pickup 

            //if (bdd == "BMS03082022")
            //{

            //    SqlCommand cmdp = new SqlCommand();

            //    SqlDataAdapter sqlDAp;
            //    Conexion cnnp = new Conexion();
            //    cnn.conexion(servidor, bdd);
            //    StringBuilder qryp = new StringBuilder();

            //    qryp.Append("SELECT  [N° DOCUMENTO] ");
            //    qryp.Append(",[Fecha_doc] as FECHA_DOC,[FechaEntrega],[cod_cte] as Cliente,[Razon_social],isnull([Tel1],'') as 'TEL1',isnull([Tel2],'')as 'TEL2',isnull([E_mail],'') as e_mail");
            //    qryp.Append(",[Peso_KG],[Latitud],[Longitud],[Direccion],[NOMBRE ITEM],[cantidad],[CODIGO ITEM],[FECHA MIN ENTREGA] ");
            //    qryp.Append(",[FECHA MAX ENTREGA],[MIN VENTANA HORARIA 1],[MAX VENTANA HORARIA 1],[MIN VENTANA HORARIA 2] ");
            //    qryp.Append(",[MAX VENTANA HORARIA 2],[COSTO ITEM],[CAPACIDAD UNO],[CAPACIDAD DOS],[SERVICE TIME],[IMPORTANCIA],[IDENTIFICADOR CONTACTO] ");
            //    qryp.Append(",[NOMBRE CONTACTO],[Telefono],[EMAIL CONTACTO],[CT ORIGEN],[REFERENCIA DOMICILIO],[calle],[numeroPuerta] ");
            //    qryp.Append(",[colonia],[barrio],[localidad],[Partido],[Estado],[Pais],[RefDomicilioExterno],[DomicilioDescripcion] ");
            //    qryp.Append(",[InicioHorario1],[InicioHorario2],[FinHorario1],[FinHorario2],[tiempoEspera],[DomCodPostal],[contacto] ");
            //    qryp.Append(",[codigoSucursal],[RefDepositoExterno],[codigoPostalEstab],[Linea],[sublinea],[RazonSocialCL],[Tipo]FROM[bms03082022].[dbo].[_Unigis_Pickup] where codigosucursal='" + codestab + "'");

            //    cmdp.CommandText = qryp.ToString();
            //    cmdp.CommandType = CommandType.Text;
            //    cmdp.CommandTimeout = 0;
            //    cmdp.Connection = cnn.conexion(servidor, bdd);
            //    sqlDAp = new SqlDataAdapter(cmdp);
            //    sqlDAp.Fill(dataTablep);



            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dtaux;

        }

        public DataTable SelecDocumentos(string establecimiento, string cod_estab)
        {
            DataTable dataTable = new DataTable();
            StringBuilder qry = new StringBuilder();
            qry.Append("select CONCAT(RTRIM(T.abreviatura),'-' ,PPE.folio)AS Documento,PPE.folio,PPE.transaccion  , COALESCE(F.cod_estab,MI.cod_estab,'') AS Estab ,E.nombre,PPE.unidad, P.cod_prod, PPE.cantidad, P.descripcion, PPE.Status from productos_por_embarcar ppe ");
            qry.Append("left join facremtick f on f.folio = ppe.folio  and f.transaccion = ppe.transaccion ");
            qry.Append("LEFT JOIN movimientos_internos MI ON MI.folio = PPE.folio AND MI.transaccion = PPE.transaccion ");
            qry.Append("left join transacciones t on t.transaccion = PPE.transaccion ");
            qry.Append("LEFT JOIN establecimientos E ON E.cod_estab = COALESCE(F.cod_estab, MI.cod_estab, '') ");
            qry.Append("LEFT JOIN PRODUCTOS P ON P.cod_prod = PPE.cod_prod where ppe.Status = '1' and E.cod_estab = '" + cod_estab + "'");

            dataTable = Consultar(qry.ToString(), cod_estab);





            return dataTable;

        }
        public DataTable Cod_estab (string establecimiento)
        {
            DataTable dtaux = new DataTable();
            DataTable dataTablep = new DataTable();
            SqlCommand cmd2 = new SqlCommand();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDA2;
            Conexion cnn2 = new Conexion();
            cnn2.conexion();
            StringBuilder qry2 = new StringBuilder();
            qry2.Append("select  e.cod_estab   from Eyp_Servidores es ");
            qry2.Append("left join establecimientos e on es.Cod_estab=e.cod_estab where bbdd is not null ");
            qry2.Append("and e.nombre='" + establecimiento + "'");
            cmd2.CommandText = qry2.ToString();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandTimeout = 0;
            cmd2.Connection = cnn2.conexion();
            sqlDA2 = new SqlDataAdapter(cmd2);
            sqlDA2.Fill(dataTable2);
            cnn2.cerrarconexion(cnn2.conexion());
            return dataTable2;

        }



        public DataTable selectitemsKG(string establecimiento)
        {
            DataTable dtaux = new DataTable();
            DataTable dataTablep = new DataTable();
            SqlCommand cmd2 = new SqlCommand();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDA2;
            Conexion cnn2 = new Conexion();
            cnn2.conexion();
            StringBuilder qry2 = new StringBuilder();
            qry2.Append("select es.servidor,es.bbdd,e.cod_estab,es.unigis  from Eyp_Servidores es ");
            qry2.Append("left join establecimientos e on es.Cod_estab=e.cod_estab where bbdd is not null ");
            qry2.Append("and e.nombre='" + establecimiento + "'");
            cmd2.CommandText = qry2.ToString();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandTimeout = 0;
            cmd2.Connection = cnn2.conexion();
            sqlDA2 = new SqlDataAdapter(cmd2);
            sqlDA2.Fill(dataTable2);
            cnn2.cerrarconexion(cnn2.conexion());
            string servidor = "";
            string bdd = "";
            string codestab = "";
            foreach (DataRow dr in dataTable2.Rows)
            {
                servidor = dr[0].ToString();
                bdd = dr[1].ToString();
                codestab = dr[2].ToString();
            }
            cnn2.cerrarconexion(cnn2.conexion());
            if (servidor == "192.168.8.4,9001")
            {
                bdd = "BMS";
            }


            //if (servidor.Trim().ToUpper() == "192.168.18.99\\BMS")
            //{
            //    servidor = "192.168.8.4,9001";
            //    bdd = "BMS03082022";
            //}


            //if (codestab.Trim() == "21")
            //{
            //    servidor = "192.168.8.4,9001";
            //    bdd = "BMS03082022";

            //}

            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("EXEC EYP_UNIGIS_VIEW'" + codestab + "' ");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTableD);
            dtaux.Merge(dataTableD, true);




            //llenar con pickup 

            //if (bdd == "BMS03082022")
            //{

            //    SqlCommand cmdp = new SqlCommand();

            //    SqlDataAdapter sqlDAp;
            //    Conexion cnnp = new Conexion();
            //    cnn.conexion(servidor, bdd);
            //    StringBuilder qryp = new StringBuilder();

            //    qryp.Append("SELECT  [N° DOCUMENTO] ");
            //    qryp.Append(",[Fecha_doc] as FECHA_DOC,[FechaEntrega],[cod_cte] as Cliente,[Razon_social],isnull([Tel1],'') as 'TEL1',isnull([Tel2],'')as 'TEL2',isnull([E_mail],'') as e_mail");
            //    qryp.Append(",[Peso_KG],[Latitud],[Longitud],[Direccion],[NOMBRE ITEM],[cantidad],[CODIGO ITEM],[FECHA MIN ENTREGA] ");
            //    qryp.Append(",[FECHA MAX ENTREGA],[MIN VENTANA HORARIA 1],[MAX VENTANA HORARIA 1],[MIN VENTANA HORARIA 2] ");
            //    qryp.Append(",[MAX VENTANA HORARIA 2],[COSTO ITEM],[CAPACIDAD UNO],[CAPACIDAD DOS],[SERVICE TIME],[IMPORTANCIA],[IDENTIFICADOR CONTACTO] ");
            //    qryp.Append(",[NOMBRE CONTACTO],[Telefono],[EMAIL CONTACTO],[CT ORIGEN],[REFERENCIA DOMICILIO],[calle],[numeroPuerta] ");
            //    qryp.Append(",[colonia],[barrio],[localidad],[Partido],[Estado],[Pais],[RefDomicilioExterno],[DomicilioDescripcion] ");
            //    qryp.Append(",[InicioHorario1],[InicioHorario2],[FinHorario1],[FinHorario2],[tiempoEspera],[DomCodPostal],[contacto] ");
            //    qryp.Append(",[codigoSucursal],[RefDepositoExterno],[codigoPostalEstab],[Linea],[sublinea],[RazonSocialCL],[Tipo]FROM[bms03082022].[dbo].[_Unigis_Pickup] where codigosucursal='" + codestab + "'");

            //    cmdp.CommandText = qryp.ToString();
            //    cmdp.CommandType = CommandType.Text;
            //    cmdp.CommandTimeout = 0;
            //    cmdp.Connection = cnn.conexion(servidor, bdd);
            //    sqlDAp = new SqlDataAdapter(cmdp);
            //    sqlDAp.Fill(dataTablep);



            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dtaux;

        }


        public string Empleado(string nemp)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("select nombre  from usuarios where usuario='" + nemp + "'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());


            return dataTable.Rows[0].ItemArray[0].ToString();

        }
        public string CEmpleado(string nemp)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("select clave_secreta  from usuarios where usuario='" + nemp + "'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());

          

            return dataTable.Rows[0].ItemArray[0].ToString().Trim();
        }

        public string NEmpleado(string nemp)
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("select tipo_usuario  from usuarios where usuario='" + nemp + "'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());


            return dataTable.Rows[0].ItemArray[0].ToString();

        }



        public DataTable Ruta(string x,string usuario   )
        {

            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            qry.Append(" SELECT IdRuta,IdJornada,Fecha_Jornada ,ruta,r.Sucursal,Operacion FROM  EYP_Unis_ruta r");
            qry.Append(" left join Eyp_Servidores eyp on eyp.unigis=r.DepositoSalida");
            qry.Append(" inner join establecimientos_usuario eu on eyp.Cod_estab=eu.cod_estab where eu.usuario='"+usuario+ "'and eu.acceso='1' ");
            if (x == "0")
            {
            qry.Append("and idruta not in (select IdRuta from EYP_Unis_ruta where Estado = 'Documentado')");
            }

            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));


            return dataTable;

        }
        public DataTable Consultar(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            qry.Append(sql);
         

            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));
            return dataTable;
        }
        public DataTable Consultar(string sql,string cod_estab)
        {
            DataTable dtaux = new DataTable();
            DataTable dataTablep = new DataTable();
            SqlCommand cmd2 = new SqlCommand();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDA2;
            Conexion cnn2 = new Conexion();
            cnn2.conexion();
            StringBuilder qry2 = new StringBuilder();
            qry2.Append("select es.servidor,es.bbdd,e.cod_estab  from Eyp_Servidores es ");
            qry2.Append("left join establecimientos e on es.Cod_estab=e.cod_estab where bbdd is not null ");
            qry2.Append("and e.cod_estab='" + cod_estab + "' or es.unigis='"+cod_estab+"'");
            cmd2.CommandText = qry2.ToString();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandTimeout = 0;
            cmd2.Connection = cnn2.conexion();
            sqlDA2 = new SqlDataAdapter(cmd2);
            sqlDA2.Fill(dataTable2);
            cnn2.cerrarconexion(cnn2.conexion());
            string servidor = "";
            string bdd = "";
            string codestab = "";
            foreach (DataRow dr in dataTable2.Rows)
            {
                servidor = dr[0].ToString();
                bdd = dr[1].ToString();
                codestab = dr[2].ToString();
            }
            cnn2.cerrarconexion(cnn2.conexion());

            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion(servidor ,bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append(sql);


            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dataTable;
        }




        public void insertarItems(int id, int route_id, string name, int quantity, int original_quantity, int delivered_quantity, string code, string description)
        {
            using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4,9001 ; Initial Catalog = BMS; User ID = sa; Password = @Sistemas74"))
            {
                string saveStaff = "Insert into EYP_BEETRACK_DESP_ITEMS values ( " + id + " ," + route_id + ",'" + name.Replace("'", "") + "','" + description.Replace("'", "") + "'," + quantity + "," + original_quantity + "," + delivered_quantity + ",'" + code + "')";

                using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                {
                    querySaveStaff.Connection = openCon;

                    openCon.Open();

                    querySaveStaff.ExecuteNonQuery();


                }

            }
        }


        public void Historico_envio(string N_Documento, string Transaccion, string Producto, string fecha_doc, string cantidad, string fecha_envio, string deposito, string usuario, string latitud, string longitud, string fecha_entrega)
        {
            using (SqlConnection openCon = new SqlConnection("Data Source = 192.168.8.4,9001 ; Initial Catalog = BMS; User ID = sa; Password = @Sistemas74"))
            {
                string saveStaff = "Insert into EYP_unigis_Doctos_Historico values ('" + N_Documento + "' ,'" + Transaccion + "','" + Producto + "','" + fecha_doc + "','" + cantidad + "','" + fecha_envio + "','" + deposito + "','" + usuario + "','" + latitud + "','" + longitud + "','" + fecha_entrega + "')";

                using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                {
                    querySaveStaff.Connection = openCon;

                    openCon.Open();

                    querySaveStaff.ExecuteNonQuery();


                }

            }
        }
        public DataTable catalogoEstab()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("select e.nombre,e.cod_estab from Eyp_Servidores es ");
            qry.Append(" left join establecimientos e on es.Cod_estab=e.cod_estab");
            qry.Append("  order by cast (e.cod_estab as int )");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());
            return dataTable;
        }

        public string Liberar_Ruta(string ruta)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            qry.Append("Update UR Set UR.Estado='Documentado' " +
                "FROM  EYP_Unis_ruta UR where idRuta='"+ruta+"'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));


            return "";
        }


        public DataTable catalogoTrans()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("Select  distinct t.nombre,t.transaccion from facremtick f ");
            qry.Append(" inner Join transacciones t on t.transaccion=f.transaccion");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());
            return dataTable;
        }
        public DataTable Viaje(string x, string u)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            qry.Append("SELECT eyp.idviaje as Viaje, Fecha, Estado, TipoObjeto, isnull((SELECT STUFF((select ',' + referenciaExterna from eyp_unis_parada where estado = 'Validado'  and idviaje = eyp.idviaje");
            qry.Append(" FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(4000)'), 1, 1, '')),'') AS Referencias, isnull(Deposito_Ref,'0') as Deposito FROM EYP_Unis_parada eyp   where tipoobjeto = 'Viaje'");
            if (x == "0")
            {
            qry.Append(" and IdViaje not in (select IdViaje from EYP_Unis_parada where IdViaje=eyp.IdViaje and TipoObjeto='Viaje' and Estado ='Liberado') ");
            }
            qry.Append("order by Fecha");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));





            return Deposito_viaje(dataTable, u); ;
        }

        public DataTable ViajeV(string x, string u)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            qry.Append("SELECT distinct eyp.idviaje as Viaje, Fecha, case when v.pendiente>0 then 'Pendiente'else 'Validado' end as Estado, TipoObjeto, isnull((SELECT STUFF((select ',' + referenciaExterna from eyp_unis_parada ");
            qry.Append(" where estado = 'Validado'  and idviaje = eyp.idviaje FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(4000)'), 1, 1, '')),'') AS Referencias, ");
            qry.Append("isnull(Deposito_Ref,'0') as Deposito FROM EYP_Unis_parada eyp ");
            qry.Append(" outer apply (select count (1) as pendiente  from EYP_Unis_parada EYPV ");
            qry.Append(" left join EYP_Unis_parada eyp2 on EYPV.IdParada=eyp2.IdParada and EYPV.IdViaje=eyp2.IdViaje and eyp2.Estado='Validado' ");
            qry.Append(" where EYPV.IdViaje=eyp.IdViaje and EYPV.IdParada<>0 and EYPV.Estado<>'Validado' and eyp2.IdParada is null) as V  ");
            qry.Append(" where tipoobjeto = 'Viaje' ");
            if (x == "0")
            {
                qry.Append(" and  v.pendiente>0 ");
            }
            qry.Append("order by  cast(IdViaje as int) desc,Fecha desc ");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));





            return Deposito_viaje(dataTable, u); ;
        }
        public DataTable consultarviaje(int viajeid)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            //qry.Append("select IdViaje,IdParada,ReferenciaExterna,Estado,Deposito_ref from EYP_Unis_parada where IdViaje='"+viajeid.ToString()+"' and TipoObjeto='Parada' and Estado in ('Entregado','Entrega parcial', 'No Entregado' ) ");
            qry.Append("select eu.IdViaje,eu.IdParada,eu.ReferenciaExterna,case when t.Estado is null then eu.Estado else t.Estado end as Estado ");
            qry.Append("from EYP_Unis_parada eu ");
            qry.Append("outer apply( ");
            qry.Append("select distinct IdParada, IdViaje, Estado from ");
            qry.Append("EYP_Unis_parada eu2 where eu.IdParada = eu2.IdParada and eu2.IdViaje = eu.IdViaje and eu2.Estado = 'Validado') t ");
            qry.Append("CROSS APPLY(SELECT TOP 1  IdParada,IdViaje,ESTADO , ID from EYP_Unis_parada eu2 where eu.IdParada = eu2.IdParada and eu2.IdViaje = eu.IdViaje And eu2.Estado in ('Entregado', 'Entrega parcial', 'No Entregado') ORDER BY ID DESC) T2 ");
            qry.Append("where eu.IdViaje = '"+viajeid.ToString()+ "' and eu.TipoObjeto = 'Parada' and eu.Estado in ('Entregado', 'Entrega parcial', 'No Entregado') AND EU.ID=T2.ID ");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));

            return dataTable;

        }

        public DataTable Deposito_viaje(DataTable dt,string u )
        {
             
            DataTable depEmp = new DataTable();
            depEmp = Depositos_usuarios(u);

            foreach (DataRow row in dt.Rows) {

                ConsultaViajeID opr = new ConsultaViajeID();
                opr.ApiKey = "1234";
                opr.IdViaje =Convert.ToInt32(row[0].ToString())  ;
                string jsonString = JsonConvert.SerializeObject(opr);
                bool f = false;
                foreach (DataRow row2 in depEmp.Rows)
                {
                    if (row2[0].ToString() == row.ItemArray[5].ToString())
                    {
                        f = true;
                    }
                }
                if (depEmp.Rows.Count == 0 || !f)
                {
                    row.Delete();
                }

             
                
                
            }
            return dt;

        }

        public string Deposito_viaje(  string viaje)
        {

                ConsultaViajeID opr = new ConsultaViajeID();
                opr.ApiKey = "1234";
            opr.IdViaje = Convert.ToInt32(viaje);
                string jsonString = JsonConvert.SerializeObject(opr);
                using (var client = new HttpClient())
                {

                    var endpoint = new Uri("https://grupo-eyp.unigis.com/mapi/soap/logistic/service.asmx/ConsultarViaje");
                    //var result = client.GetAsync(endpoint).Result;
                    //var xmlR = result.Content.ReadAsStringAsync().Result;

                    var paylod = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, paylod).Result.Content.ReadAsStringAsync().Result;
                    // MessageBox.Show(result);


                    string deposito = JsonConvert.DeserializeObject<ObtenerViaje_Response.Rootobject>(result).d.DepositoSalida.RefDepositoExterno.ToString();
                return deposito;
                     

                }
                 
             

        }

        public DataTable ParadasViaje(string idViaje)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion(  );
            StringBuilder qry = new StringBuilder();
            qry.Append("select distinct IdParada from EYP_Unis_parada where TipoObjeto='Parada' and IdViaje='" + idViaje + "'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion( );
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion( ));
            return dataTable;
        }
        public DataTable CoordenadasDeposito(string Deposito)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion("192.168.8.4,9001", "BMS");
            StringBuilder qry = new StringBuilder();
            qry.Append("select Latitud,Longitud from Eyp_Servidores es ");
            qry.Append("left join Eyp_Establecimiento_coordenadas ec on es.Cod_estab = ec.cod_estab ");
            qry.Append(" where es.Unigis = '"+Deposito+"'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion("192.168.8.4,9001", "BMS");
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion("192.168.8.4,9001", "BMS"));
            return dataTable;
        }
        public DataTable ValesPendientes()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();
            cnn.conexion();
            StringBuilder qry = new StringBuilder();
            qry.Append("select vm.folio_origen as Folio_Docto,t.abreviatura as Transaccion,Fecha,vm.folio as Vale ,EO.nombre as Estab_Origen ,ED.nombre as Estab_Destino from vales_mercancia vm ");
            qry.Append("inner join establecimientos EO on EO.cod_estab = vm.cod_estab_emision ");
            qry.Append("inner join establecimientos ED on ED.cod_estab = vm.cod_estab ");
            qry.Append("inner join transacciones t on t.transaccion = vm.transaccion_origen ");
            qry.Append("where vm.folio_destino = '' and vm.transaccion_destino = '' and vm.cod_estab <> vm.cod_estab_emision order by vm.fecha");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable);
            cnn.cerrarconexion(cnn.conexion());

            DataTable dataTable2 = new DataTable();
            qry.Clear();
            qry.Append("select vm.folio_origen as Folio_Docto,t.abreviatura as Transaccion,Fecha,vm.folio as Vale ,EO.nombre as Estab_Origen ,ED.nombre as Estab_Destino from [ENS_SERVCONTA\\BMS2].BMSGBC.DBO.vales_mercancia vm ");
            qry.Append("inner join [ENS_SERVCONTA\\BMS2].BMSGBC.DBO.establecimientos EO on EO.cod_estab = vm.cod_estab_emision ");
            qry.Append("inner join [ENS_SERVCONTA\\BMS2].BMSGBC.DBO.establecimientos ED on ED.cod_estab = vm.cod_estab ");
            qry.Append("inner join [ENS_SERVCONTA\\BMS2].BMSGBC.DBO.transacciones t on t.transaccion = vm.transaccion_origen ");
            qry.Append("where vm.folio_destino = '' and vm.transaccion_destino = '' and vm.cod_estab <> vm.cod_estab_emision order by vm.fecha");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion();
            sqlDA = new SqlDataAdapter(cmd);
            sqlDA.Fill(dataTable2);
            cnn.cerrarconexion(cnn.conexion());

            dataTable.Merge(dataTable2);
            return dataTable;
        }
        public void UpdatePPE(string cod_prod, string documento, string transaccion, string establecimiento, string status)
        {
            DataTable dtaux = new DataTable();
            DataTable dataTablep = new DataTable();
            SqlCommand cmd2 = new SqlCommand();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDA2;
            Conexion cnn2 = new Conexion();
            cnn2.conexion();
            StringBuilder qry2 = new StringBuilder();
            qry2.Append("select es.servidor,es.bbdd,e.cod_estab  from Eyp_Servidores es ");
            qry2.Append("left join establecimientos e on es.Cod_estab=e.cod_estab where bbdd is not null ");
            qry2.Append("and e.cod_estab='" + establecimiento + "'");
            cmd2.CommandText = qry2.ToString();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandTimeout = 0;
            cmd2.Connection = cnn2.conexion();
            sqlDA2 = new SqlDataAdapter(cmd2);
            sqlDA2.Fill(dataTable2);
            cnn2.cerrarconexion(cnn2.conexion());
            string servidor = "";
            string bdd = "";
            string codestab = "";
            foreach (DataRow dr in dataTable2.Rows)
            {
                servidor = dr[0].ToString();
                bdd = dr[1].ToString();
                codestab = dr[2].ToString();
            }
            cnn2.cerrarconexion(cnn2.conexion());

            using (SqlConnection openCon = new SqlConnection("Data Source = " + servidor + " ; Initial Catalog = " + bdd + "; User ID = sa; Password = @Sistemas74"))
            {
                string saveStaff = " update productos_por_embarcar set status='" + status + "' where folio='" + documento.Trim() + "' and cod_prod='" + cod_prod.Trim() + "' and transaccion='" + transaccion.Trim() + "' ";

                using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                {
                    querySaveStaff.Connection = openCon;

                    openCon.Open();

                    querySaveStaff.ExecuteNonQuery();
                    openCon.Close();
                }
            }

        }
        public DataTable Motivos(string establecimiento)
        {
            DataTable dtaux = new DataTable();
            DataTable dataTablep = new DataTable();
            SqlCommand cmd2 = new SqlCommand();
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDA2;
            Conexion cnn2 = new Conexion();
         
            
            
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";

            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("select CONCAT(rtrim(razon_no_surtido),'|',right(nombre,DATALENGTH(nombre)-2)) from  razones_no_surtido");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTableD);
            //dtaux.Merge(dataTableD, true);
            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dataTableD;

        }

        public DataTable Depositos_usuarios(string u)
        {
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";
            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append(" SELECT Unigis FROM    Eyp_Servidores eyp ");
            qry.Append("inner join establecimientos_usuario eu on eyp.Cod_estab = eu.cod_estab where eu.usuario = '"+u+"' and eu.acceso = '1'   and Unigis is not null ");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTableD);
            //dtaux.Merge(dataTableD, true);
            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dataTableD;

        }

        public string EstatusParada( string referencia )
        {
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";
            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("select Estado from EYP_Unis_parada where ReferenciaExterna='"+referencia+"' and Estado not in('Validado','Liberado')");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTableD);
            //dtaux.Merge(dataTableD, true);
            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dataTableD.Rows[0][0].ToString();
        }
        //private static string ToLiteral(string valueTextForCompiler)
        //{
        //    return Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false);
        //}
        public void ActualizarViaje(string idviaje,string usuario)
        {
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";
            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("EYP_LiberaParadasUnigis");
            cmd.CommandText = qry.ToString();
            cmd.Parameters.Add("@idViaje", idviaje);
            cmd.Parameters.Add("@usuario", usuario);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
              
            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
             
        }
        public void ActualizarRuta(string idjornada, string idruta,string usuario )
        {
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";
            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("EYP_DocumentarRutasUnigis");
            cmd.CommandText = qry.ToString();
            cmd.Parameters.Add("@idruta", idruta);
            cmd.Parameters.Add("@idjornada", idjornada);
            cmd.Parameters.Add("@usuario", usuario);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            cnn.cerrarconexion(cnn.conexion(servidor, bdd));

        }

        public string CancelarLiq(string folio, string usuario)
        {
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";
            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("exec EYP_CANCELARLIQ '" + folio + "','" + usuario + "'");
            cmd.CommandText = qry.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            sqlDA = new SqlDataAdapter(cmd);

            sqlDA.Fill(dataTableD);
            //dtaux.Merge(dataTableD, true);
            cnn.cerrarconexion(cnn.conexion(servidor, bdd));
            return dataTableD.Rows[0][0].ToString();

        }
        public void ActualizarFechaEntrega(string folio, string transaccion,string fecha, string usuario)
        {
            string servidor = "192.168.8.4,9001";
            string bdd = "BMS";
            SqlCommand cmd = new SqlCommand();
            DataTable dataTableD = new DataTable();
            SqlDataAdapter sqlDA;
            Conexion cnn = new Conexion();

            cnn.conexion(servidor, bdd);
            StringBuilder qry = new StringBuilder();
            qry.Append("EYP_ActFechaDocto");
            cmd.CommandText = qry.ToString();
            cmd.Parameters.Add("@Folio", folio);
            cmd.Parameters.Add("@Transaccion", transaccion);
            cmd.Parameters.Add("@fecha", fecha);
            cmd.Parameters.Add("@usuario", usuario);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = cnn.conexion(servidor, bdd);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            cnn.cerrarconexion(cnn.conexion(servidor, bdd));

        }

    }

}


