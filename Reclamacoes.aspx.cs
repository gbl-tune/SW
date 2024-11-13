using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SW
{
    public partial class Reclamacoes : System.Web.UI.Page
    {
        string id_C;

        string connectionString = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserCategory"] == null || Session["UserCategory"].ToString() == "DATABASEMANAGEROFFICER" || Session["UserCategory"].ToString() == "USERPAP")
                {
                    Response.Redirect("~/Login.aspx");
                }
                CarregarGridView();
                carregarPAP();
            }
        }

        private void CarregarGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Queixas ", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int novoQueixaId;
            string novoQueixaCode = string.Empty;

            if (Page.IsValid)
            {
                // Definir a conexão com o banco de dados (você deve ajustar a string de conexão conforme seu ambiente)

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                        // Criação da query SQL com parâmetros
                        string query = @"
                    INSERT INTO Queixas (
                    [C/S], DateComplaint,HHName,HHContact,RelationshipHH, PAPid, NameOfComplainant,  Distrit,DistritIncident, 
                    ProjectRelated,Bairro,BairroIncident,Zona,ZonaIncident,Quarteirao,QuarteiraoIncident, Method  ,TypeofComplaint, 
                     Gender, PhoneNumber, Status
                    ) OUTPUT INSERTED.ComplaintNr VALUES (@CS,
                     @DateComplaint,@HHName,@HHContact,@RelationshipHH, @PAPid, @NameOfComplainant, @Distrit,@DistritIncident,@ProjectRelated, 
                    @Bairro,@BairroIncident,@Zona,@ZonaIncident,@Quarteirao,@QuarteiraoIncident,@Method, 
                    @TypeofComplaint ,
                    @Gender, @PhoneNumber,  'Registada')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adiciona os parâmetros utilizando diretamente os IDs dos campos
                        //command.Parameters.AddWithValue("@ComplaintNr", string.IsNullOrEmpty(complaintNr.Value) ? DBNull.Value : (object)complaintNr.Value);
                        command.Parameters.AddWithValue("@CS", string.IsNullOrEmpty(tipo.Value) ? DBNull.Value : (object)tipo.Value);
                        command.Parameters.AddWithValue("@DateComplaint", string.IsNullOrEmpty(dateComplaint.Value) ? DBNull.Value : (object)dateComplaint.Value);
                        command.Parameters.AddWithValue("@PAPid", string.IsNullOrEmpty(ddlSearch.SelectedValue) ? DBNull.Value : (object)ddlSearch.SelectedValue);
                        command.Parameters.AddWithValue("@Confidentiality", string.IsNullOrEmpty(confidentiality.Value) ? DBNull.Value : (object)confidentiality.Value);
                        command.Parameters.AddWithValue("@HHName", string.IsNullOrEmpty(HHname.Value) ? DBNull.Value : (object)HHname.Value);
                        command.Parameters.AddWithValue("@HHContact", string.IsNullOrEmpty(HHContact.Value) ? DBNull.Value : (object)HHContact.Value);
                        command.Parameters.AddWithValue("@NameOfComplainant", string.IsNullOrEmpty(nameOfComplainant.Value) ? DBNull.Value : (object)nameOfComplainant.Value);
                        command.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender.Value) ? DBNull.Value : (object)gender.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber.Value) ? DBNull.Value : (object)phoneNumber.Value);
                        command.Parameters.AddWithValue("@RelationshipHH", string.IsNullOrEmpty(parentesco.Value) ? DBNull.Value : (object)parentesco.Value);


                        command.Parameters.AddWithValue("@Distrit", string.IsNullOrEmpty(distrit.Value) ? DBNull.Value : (object)distrit.Value);
                        command.Parameters.AddWithValue("@DistritIncident", string.IsNullOrEmpty(distritoC.Value) ? DBNull.Value : (object)distritoC.Value);
                        command.Parameters.AddWithValue("@Bairro", string.IsNullOrEmpty(bairro.Value) ? DBNull.Value : (object)bairro.Value);
                        command.Parameters.AddWithValue("@BairroIncident", string.IsNullOrEmpty(bairroC.Value) ? DBNull.Value : (object)bairroC.Value);
                        command.Parameters.AddWithValue("@Zona", string.IsNullOrEmpty(zona.Value) ? DBNull.Value : (object)zona.Value);
                        command.Parameters.AddWithValue("@ZonaIncident", string.IsNullOrEmpty(zonaC.Value) ? DBNull.Value : (object)zonaC.Value);
                        command.Parameters.AddWithValue("@Quarteirao", string.IsNullOrEmpty(quarteirao.Value) ? DBNull.Value : (object)quarteirao.Value);
                        command.Parameters.AddWithValue("@QuarteiraoIncident", string.IsNullOrEmpty(quarteiraoC.Value) ? DBNull.Value : (object)quarteiraoC.Value);

                        command.Parameters.AddWithValue("@ProjectRelated", string.IsNullOrEmpty(projectRelated.Value) ? DBNull.Value : (object)projectRelated.Value);
                        command.Parameters.AddWithValue("@Method", outro.Checked ? (object)otherMethod.Value : reportingMethod.Value);

                        // Outros campos

                        command.Parameters.AddWithValue("@TypeofComplaint", outrotipo.Checked ? (object)outraClassificacao.Value : typeOfComplaint.Value);



                        //command.Parameters.AddWithValue("@RedressDate", string.IsNullOrEmpty(redressDate.Value) ? DBNull.Value : (object)redressDate.Value);
                        //command.Parameters.AddWithValue("@ProposedDecision", string.IsNullOrEmpty(proposedDecision.Value) ? DBNull.Value : (object)proposedDecision.Value);
                        //command.Parameters.AddWithValue("@Outcome", string.IsNullOrEmpty(outcome.Value) ? DBNull.Value : (object)outcome.Value);
                        ////command.Parameters.AddWithValue("@ComplainantName", string.IsNullOrEmpty(complainantName.Value) ? DBNull.Value : (object)complainantName.Value);
                        //command.Parameters.AddWithValue("@FinalDecision", string.IsNullOrEmpty(finalDecision.Value) ? DBNull.Value : (object)finalDecision.Value);
                        //command.Parameters.AddWithValue("@Authority", string.IsNullOrEmpty(authority.Value) ? DBNull.Value : (object)authority.Value);
                        //command.Parameters.AddWithValue("@RemedialDate", string.IsNullOrEmpty(remedialDate.Value) ? DBNull.Value : (object)remedialDate.Value);
                        //command.Parameters.AddWithValue("@ActualRemedialDate", string.IsNullOrEmpty(actualremedialDate.Value) ? DBNull.Value : (object)actualremedialDate.Value);
                        //command.Parameters.AddWithValue("@PersonVerifying", string.IsNullOrEmpty(personVerifying.Value) ? DBNull.Value : (object)personVerifying.Value);
                        //command.Parameters.AddWithValue("@CloseOut", string.IsNullOrEmpty(closeOUt.Value) ? DBNull.Value : (object)closeOUt.Value);
                        
                        
                        //command.Parameters.AddWithValue("@Escalation", string.IsNullOrEmpty(escalation.Value) ? DBNull.Value : (object)escalation.Value);
                        //command.Parameters.AddWithValue("@EntityReferred", string.IsNullOrEmpty(entityReferred.Value) ? DBNull.Value : (object)entityReferred.Value);
                        //command.Parameters.AddWithValue("@Contact", string.IsNullOrEmpty(contact.Value) ? DBNull.Value : (object)contact.Value);
                        //command.Parameters.AddWithValue("@DateMentioned", string.IsNullOrEmpty(dateMentioned.Value) ? DBNull.Value : (object)dateMentioned.Value);
                        //command.Parameters.AddWithValue("@RemedialAction", string.IsNullOrEmpty(remedialAction.Value) ? DBNull.Value : (object)remedialAction.Value);
                        //command.Parameters.AddWithValue("@CloseDate", string.IsNullOrEmpty(closeDate.Value) ? DBNull.Value : (object)closeDate.Value);


                        connection.Open();
                        novoQueixaId = (int)command.ExecuteScalar(); // Retorna o QueixaId gerado\
                    }

                        // Gera o código alfanumérico com base no ID gerado (ex.: "Q001", "Q002")
                        novoQueixaCode = $"Q{novoQueixaId:0000}";
                        string queryAtualizarQueixa = "UPDATE Queixas SET ComplaintCode = @QueixaCode WHERE ComplaintNr = @QueixaId";

                        using (SqlCommand cmdAtualizar = new SqlCommand(queryAtualizarQueixa, connection))
                        {
                            cmdAtualizar.Parameters.AddWithValue("@QueixaCode", novoQueixaCode);
                            cmdAtualizar.Parameters.AddWithValue("@QueixaId", novoQueixaId);
                            cmdAtualizar.ExecuteNonQuery(); // Executa a atualização
                        }

                        // Abrir conexão e executar o comando
                        //command.ExecuteNonQuery();
                        CarregarGridView();


                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                //Verifica que botao foi pressionado comparando o CommnadName
                if (e.CommandName == "Editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    id_C = row.Cells[1].Text;
                    //complaintNrE.Value = id_C;
                    

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Queixas where ComplaintNr='" + id_C + "'");
                        con.Open();
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            tipoE.Value = !string.IsNullOrEmpty(sdr["C/S"].ToString())? sdr["C/S"].ToString():"";///////
                            complaintNrE.Value = sdr["ComplaintCode"].ToString().Trim();
                            DateTime dateValueI = Convert.ToDateTime(sdr["DateComplaint"]);
                            dateComplaintE.Value = dateValueI.ToString("yyyy-MM-dd");

                            PAPidE.Value = sdr["PAPid"].ToString();//////
                            confidentialityE.Value = sdr["Confidentiality"].ToString();//////
                            HHnameE.Value = !string.IsNullOrEmpty(sdr["HHName"].ToString())? sdr["HHName"].ToString():"";
                            HHcontactE.Value = !string.IsNullOrEmpty(sdr["HHContact"].ToString())? sdr["HHContact"].ToString():"";
                            nameOfComplainantE.Value = sdr["NameOfComplainant"].ToString();
                            genderE.Value = sdr["Gender"].ToString();
                            phoneNumberE.Value = sdr["PhoneNumber"].ToString();                          
                            parentescoE.Value = !string.IsNullOrEmpty(sdr["RelationshipHH"].ToString()) ? sdr["RelationshipHH"].ToString() : "";   
                            distritoE.Value = !string.IsNullOrEmpty(sdr["Distrit"].ToString()) ? sdr["Distrit"].ToString() : "";   
                            distritoCE.Value = !string.IsNullOrEmpty(sdr["DistritIncident"].ToString()) ? sdr["DistritIncident"].ToString() : "";   
                            bairroE.Value = !string.IsNullOrEmpty(sdr["Bairro"].ToString()) ? sdr["Bairro"].ToString() : "";   
                            bairroCE.Value = !string.IsNullOrEmpty(sdr["BairroIncident"].ToString()) ? sdr["BairroIncident"].ToString() : "";   
                            zonaE.Value = !string.IsNullOrEmpty(sdr["Zona"].ToString()) ? sdr["Zona"].ToString() : "";   
                            zonaCE.Value = !string.IsNullOrEmpty(sdr["ZonaIncident"].ToString()) ? sdr["ZonaIncident"].ToString() : "";   
                            quarteiraoE.Value = !string.IsNullOrEmpty(sdr["Quarteirao"].ToString()) ? sdr["Quarteirao"].ToString() : "";   
                            quarteiraoCE.Value = !string.IsNullOrEmpty(sdr["QuarteiraoIncident"].ToString()) ? sdr["QuarteiraoIncident"].ToString() : "";   
                            cooredenadasE.Value = !string.IsNullOrEmpty(sdr["Coordenadas"].ToString()) ? sdr["Coordenadas"].ToString() : "";
                            coordenadasCE.Value = !string.IsNullOrEmpty(sdr["CoordenadasIncident"].ToString()) ? sdr["CoordenadasIncident"].ToString() : "";   
                            

                            projectRelatedE.Value = sdr["ProjectRelated"].ToString();
                            reportingMethodE.Value = !string.IsNullOrEmpty(sdr["Method"].ToString())? sdr["Method"].ToString():"";
                            //other

                            //internalPersonE.Value = sdr["InternalPerson"].ToString();
                            //descriptionE.Value = sdr["Description"].ToString();

                            //typeOfComplaintE.Value = !string.IsNullOrEmpty(sdr["TypeofComplaint"].ToString()) ? sdr["TypeofComplaint"].ToString():" ";
                            //other


                            if (!string.IsNullOrEmpty(sdr["RedressDate"].ToString()))
                            {
                                redressDateE.Value = Convert.ToDateTime(sdr["RedressDate"]).ToString("yyyy-MM-dd");

                            }
                            investigationOutcomeE.Value = !string.IsNullOrEmpty(sdr["InvestigarionOC"].ToString()) ? sdr["InvestigarionOC"].ToString() : "";
                            outcomeE.Value = !string.IsNullOrEmpty(sdr["Outcome"].ToString()) ? sdr["Outcome"].ToString() : "";
                            proposedDecisionE.Value = !string.IsNullOrEmpty(sdr["ProposedDecision"].ToString()) ? sdr["ProposedDecision"].ToString() : "";
                            //finalDecisionE.Value = !string.IsNullOrEmpty(sdr["FinalDecision"].ToString()) ? sdr["FinalDecision"].ToString() : "";
                            authorityE.Value = !string.IsNullOrEmpty(sdr["Authority"].ToString()) ? sdr["Authority"].ToString() : "";
                            if (!string.IsNullOrEmpty(sdr["RemedialDate"].ToString()))
                            {
                                remedialDateE.Value = Convert.ToDateTime(sdr["RemedialDate"]).ToString("yyyy-MM-dd");

                            }if (!string.IsNullOrEmpty(sdr["ActualRemedialDate"].ToString()))
                            {
                                actualremedialDateE.Value = Convert.ToDateTime(sdr["ActualRemedialDate"]).ToString("yyyy-MM-dd");

                            }
                           
                            
                            personVerifyingE.Value = !string.IsNullOrEmpty(sdr["PersonVerifying"].ToString()) ? sdr["PersonVerifying"].ToString() : "";

                            if (!string.IsNullOrEmpty(sdr["CloseOut"].ToString()))
                            {
                                closeOUtE.Value = Convert.ToDateTime(sdr["CloseOut"]).ToString("yyyy-MM-dd");

                            }
                           

                            escalationE.Value = !string.IsNullOrEmpty(sdr["Escalation"].ToString()) ? sdr["Escalation"].ToString() : "";
                            entityReferredE.Value = !string.IsNullOrEmpty(sdr["EntityReferred"].ToString()) ? sdr["EntityReferred"].ToString() : "";
                            contactE.Value = !string.IsNullOrEmpty(sdr["Contact"].ToString()) ? sdr["Contact"].ToString() : "";
                            if (!string.IsNullOrEmpty(sdr["DateMentioned"].ToString()))
                            {
                                dateMentionedE.Value = Convert.ToDateTime(sdr["DateMentioned"]).ToString("yyyy-MM-dd");

                            }

                            
                            //remedialActionE.Value = !string.IsNullOrEmpty(sdr["RemedialAction"].ToString()) ? sdr["RemedialAction"].ToString() : "";
                            if (!string.IsNullOrEmpty(sdr["CloseDate"].ToString()))
                            {
                                closeDateE.Value = Convert.ToDateTime(sdr["CloseDate"]).ToString("yyyy-MM-dd");

                            }

                            carregarddl(ddlEstadoSecG, "G", sdr["ComplaintNr"].ToString());

                            //DateTime dateValueI = Convert.ToDateTime(sdr["DataIngresso"]);
                            //dataIngresso.Value = dateValueI.ToString("yyyy-MM-dd");
                            //dataFim.Value = sdr["DataFiim"].ToString();
                            //departamento.Value = sdr["Departamento"].ToString();
                            //nivelAcademico.Value = sdr["NivelAcademico"].ToString();
                            //contacto.Value = sdr["Contacto"].ToString();

                        }




                        con.Close();


                    }




                    string script = "  $(document).ready(function() { $('#editarQueixa').modal('toggle'); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);

                }
                else 

                    if (e.CommandName == "Download")
                    {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    id_C = row.Cells[1].Text;
                    //complaintNrE.Value = id_C;






                    byte[] bytes;
                    string fileName, contentType;
                    
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                        //Response.Write("<script>alert('gjgj')</script>");
                            cmd.CommandText = "select  NameOfComplainant, FileIO, Type from Queixas where ComplaintNr=@Id";
                            cmd.Parameters.AddWithValue("@Id", id_C);
                            cmd.Connection = con;
                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                sdr.Read();
                                bytes = (byte[])sdr["FileIO"];
                                contentType = sdr["Type"].ToString();
                                fileName = sdr["NameOfComplainant"].ToString();
                            }
                            if (bytes == null)
                            {
                                Response.Write("<script>alert('Nao existe ficheiro')</script>");

                            }
                            con.Close();
                        }
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                   

                }else if (e.CommandName == "VerificarStatus")
                {
                    // Obtém o índice da linha selecionada
                    
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    
                    // Obtém o ID da queixa a partir da chave de dados
                    int queixaId = Convert.ToInt32(row.Cells[1].Text);

                    // Armazena o ID da queixa no campo oculto para uso posterior
                    hfQueixaId.Value = queixaId.ToString();

                    // Abre o modal via JavaScript
                    string script = "  $(document).ready(function() { $('#statusModal').modal('toggle'); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);
                }
            }
            catch (Exception ev)
            {
                Response.Write("<script>alert('" + ev.Message.ToString() + "')</script>");

            }
        }


        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            int queixaId = Convert.ToInt32(hfQueixaId.Value);

            // Atualiza o status da queixa para "Concluída"
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Queixas SET Status = 'Concluída' WHERE ComplaintNr = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", queixaId);

                con.Open();
                // Salvar o histórico da transição
                SqlCommand cmdHistorico = new SqlCommand("INSERT INTO HistoricoEstados (QueixaId, Estadoid,DataTransicao,Username) VALUES (@QueixaId, @Estado,@DataMudanca,@Username)", con);
                cmdHistorico.Parameters.AddWithValue("@QueixaId", queixaId);
                cmdHistorico.Parameters.AddWithValue("@Estado", VerificarEtapa(queixaId));
                cmdHistorico.Parameters.AddWithValue("@DataMudanca", DateTime.Now);
                cmdHistorico.Parameters.AddWithValue("@Username", Session["User"]);
                cmdHistorico.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
            }

            // Recarrega a lista de queixas e fecha o modal
            //CarregarQueixas();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#statusModal').modal('hide');", true);
        }


        // Método chamado quando o usuário clica em "Não, Próxima Etapa"
        protected void btnProximaEtapa_Click(object sender, EventArgs e)
        {
            int queixaId = Convert.ToInt32(hfQueixaId.Value);
            ExecutarProximaEtapa(queixaId, VerificarEtapa(queixaId));
            // Atualiza o status da queixa para "Próxima Etapa"
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = "UPDATE Queixas SET Status = 'Próxima Etapa' WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", queixaId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // Recarrega a lista de queixas e fecha o modal
            //CarregarQueixas();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#statusModal').modal('hide');", true);
        }


        private string VerificarEtapa(int id)
        {
            // Conexão com o banco de dados (substitua pela sua string de conexão)
            string status;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Status FROM Queixas where ComplaintNr="+id+"";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    status = reader["Status"].ToString();
                    return status;

                }
                reader.Close();
                
            }
            return null;
        }

        private void ExecutarProximaEtapa(int id, string statusAtual)
        {
            using (SqlConnection con = new SqlConnection("SuaStringDeConexao"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                //define quantos dias atrasou a mudanca
                //cmd.CommandText = "UPDATE Queixas SET Atraso=@Atraso WHERE Id = @Id";
                // Define a nova etapa com base no status atual
                switch (statusAtual)
                {
                    case "Grievance Notification":
                        cmd.CommandText = "UPDATE Queixas SET Status = 'Triage and cataloguing', DataInicioEtapa = @DataInicio, DiasParaProximaEtapa = 5 WHERE ComplaintNr = @Id";
                        break;

                    case "Triage and cataloguing":
                        cmd.CommandText = "UPDATE Queixas SET Status = 'Implementation of Agreement', DataInicioEtapa = @DataInicio, DiasParaProximaEtapa = 5 WHERE ComplaintNr = @Id";
                        break;

                    case "Communication w/ PAP":
                        cmd.CommandText = "UPDATE Queixas SET Status = 'Finished', DataInicioEtapa = @DataInicio, DiasParaProximaEtapa = 5 WHERE ComplaintNr = @Id";
                        break;

                    // Continue com as outras etapas conforme o fluxograma

                    default:
                        return;
                }

                // Define os parâmetros para a nova etapa
                cmd.Parameters.AddWithValue("@DataInicio", DateTime.Now);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Atraso", id);
                cmd.ExecuteNonQuery();
            }
        }
        bool clickY;
        bool clickN;

        protected void btnYes_Click(object sender, EventArgs e)
        {
            clickY=true;

        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            clickN=true;
            
        }
        public string Accepted()
        {
            if (clickY) 
            {
                return "YES";
            }
            if (clickN) 
            {
                return "NO";
            }
            return null;
        }



        protected void upload_Click(object sender, EventArgs e) {  }

        public void upload()
        {



            try
            {




                string filePath = FileUploadIO.PostedFile.FileName; // getting the file path of uploaded file  
                string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file  
                string ext = Path.GetExtension(filename1); // getting the file extension of uploaded file
                string type = String.Empty;

                if (!FileUploadIO.HasFile)
                {
                    Response.Write("Selecione Um Ficheiro");
                }
                if (FileUploadIO.HasFile)
                {
                    try
                    {
                        switch (ext) // this switch code validate the files which allow to upload only PDF file   
                        {
                            case ".pdf":
                                type = "application/pdf";
                                break;
                        }
                        if (type != String.Empty)
                        {
                            string contentType = FileUploadIO.PostedFile.ContentType;
                            using (Stream fs = FileUploadIO.PostedFile.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(fs))
                                {
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                    using (SqlConnection con = new SqlConnection(connectionString))
                                    {
                                        string query = "update Queixas set Type=@ContentType ,FileIO=@Data where ComplaintCode='" + complaintNrE.Value + "'";
                                        //string query = "update Queixas set Type=@ContentType ,FileIO=@Data where ComplaintCode='Q0003'";
                                        using (SqlCommand cmd = new SqlCommand(query))
                                        {
                                            cmd.Connection = con;
                                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                                            cmd.Parameters.AddWithValue("@Data", bytes);
                                            //cmd.Parameters.AddWithValue("@id",complaintNrE.Value);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                            }
                            Response.Redirect(Request.Url.AbsoluteUri);




                        }
                        //se for foto

                        //else
                        //{
                        //    using (SqlConnection con = new SqlConnection(connectionString))
                        //    {
                        //        byte[] myphoto = FileUploadIO.FileBytes;
                        //        if (con.State == ConnectionState.Closed)
                        //            con.Open();
                        //        string insertUplod = "update tbl_Estagiario set foto=@pic where ordem=@id";
                        //        SqlCommand insertCmd = new SqlCommand(insertUplod, con);
                        //        insertCmd.Parameters.AddWithValue("@pic", myphoto);
                        //        insertCmd.Parameters.AddWithValue("@id", complaintNrE.Value);
                        //        insertCmd.ExecuteNonQuery();
                        //        con.Close();
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message.ToString());
                    }
                }

            }
            catch (Exception evt)
            {

                Response.Write("<script>alert('" + evt.Message.ToString() + "')</script>");
            }




        }

        protected void exitModal_Click(object sender, EventArgs e)
        {
            string script = "  $(document).ready(function() { $('#editarQueixa').modal('hide'); });";

            ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);

        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            string id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("select ComplaintNr from Queixas where ComplaintCode='"+complaintNrE.Value+"'", connection))
                {
                    
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        sdr.Read();
                        id = sdr["ComplaintNr"].ToString().Trim();


                      
                    }
                }
            }
            update_All(id);

        }

        public void update_All(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                     //DateComplaint=@DateComplaint, PAPid=@PAPid,
                     //NameOfComplainant=@NameOfComplainant, ComplainantAdress=@ComplainantAdress,
                     //Comunity=@Comunity, Distrit=@Distrit, 
                     //ProjectRelated=@ProjectRelated, Method=@Method , 
                     //InternalPerson=@InternalPerson, Description=@Description,TypeofComplaint=@TypeofComplaint, 
                     //Gender=@Gender, PhoneNumber=@PhoneNumber, EmailConfidencial= @EmailConfidencial,
                // Criação da query SQL com parâmetros
                string query = @"
                    Update Queixas set HHName=@HHname,HHContact=@HHcontacto,NameOfComplainant=@NameofComplainant,Gender=@Gender,PhoneNumber=@Phone,
                     RelationshipHH=@RelationshipHH, Distrit=@Distrit, DistritIncident=@DistritIncident,Bairro=@Bairro,BairroIncident=@BairroIncident,Zona=@Zona,ZonaIncident=@ZonaIncident,
                    Quarteirao=@Quarteirao,QuarteiraoIncident=@QuarteiraoIncident,Coordenadas=@Coordenadas, CoordenadasIncident=@CoordenadasIncident,ProjectRelated=@ProjectRelated,
                     RedressDate=@RedressDate, ProposedDecision=@ProposedDecision, Outcome= @Outcome, InvestigarionOC=@InvestigarionOC,
                      Authority=@Authority, RemedialDate=@RemedialDate, 
                     ActualRemedialDate=@ActualRemedialDate, PersonVerifying= @PersonVerifying, 
                    CloseOut=@CloseOut, Escalation=@Escalation, EntityReferred=@EntityReferred, 
                    Contact=@Contact, DateMentioned= @DateMentioned, CloseDate=@CloseDate,Status='Nivel_1'
                     where ComplaintCode='" + complaintNrE.Value+"'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adiciona os parâmetros utilizando diretamente os IDs dos campos
                    


                    command.Parameters.AddWithValue("@HHname", string.IsNullOrEmpty(HHnameE.Value) ? DBNull.Value : (object)HHnameE.Value);
                    command.Parameters.AddWithValue("@HHcontacto", string.IsNullOrEmpty(HHcontactE.Value) ? DBNull.Value : (object)HHcontactE.Value);
                    command.Parameters.AddWithValue("@RelationshipHH", string.IsNullOrEmpty(parentescoE.Value) ? DBNull.Value : (object)parentescoE.Value);
                    command.Parameters.AddWithValue("@NameOfComplainant", string.IsNullOrEmpty(nameOfComplainantE.Value) ? DBNull.Value : (object)nameOfComplainantE.Value);
                    command.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(genderE.Value) ? DBNull.Value : (object)genderE.Value);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phoneNumberE.Value) ? DBNull.Value : (object)phoneNumberE.Value);
                    command.Parameters.AddWithValue("@Distrit", string.IsNullOrEmpty(distritoE.Value) ? DBNull.Value : (object)distritoE.Value);
                    command.Parameters.AddWithValue("@DistritIncident", string.IsNullOrEmpty(distritoCE.Value) ? DBNull.Value : (object)distritoCE.Value);
                    command.Parameters.AddWithValue("@Bairro", string.IsNullOrEmpty(bairroE.Value) ? DBNull.Value : (object)bairroE.Value);
                    command.Parameters.AddWithValue("@BairroIncident", string.IsNullOrEmpty(bairroCE.Value) ? DBNull.Value : (object)bairroCE.Value);
                    command.Parameters.AddWithValue("@Zona", string.IsNullOrEmpty(zonaE.Value) ? DBNull.Value : (object)zonaE.Value);
                    command.Parameters.AddWithValue("@ZonaIncident", string.IsNullOrEmpty(zonaCE.Value) ? DBNull.Value : (object)zonaCE.Value);
                    command.Parameters.AddWithValue("@Quarteirao", string.IsNullOrEmpty(quarteiraoE.Value) ? DBNull.Value : (object)quarteiraoE.Value);
                    command.Parameters.AddWithValue("@QuarteiraoIncident", string.IsNullOrEmpty(quarteiraoCE.Value) ? DBNull.Value : (object)quarteiraoCE.Value);
                    command.Parameters.AddWithValue("@Coordenadas", string.IsNullOrEmpty(cooredenadasE.Value) ? DBNull.Value : (object)cooredenadasE.Value);
                    command.Parameters.AddWithValue("@CoordenadasIncident", string.IsNullOrEmpty(coordenadasCE.Value) ? DBNull.Value : (object)coordenadasCE.Value);
                    //command.Parameters.AddWithValue("@Confidentiality", string.IsNullOrEmpty(confidentiality.Value) ? DBNull.Value : (object)confidentiality.Value);
                    //command.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender.Value) ? DBNull.Value : (object)gender.Value);
                    //command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber.Value) ? DBNull.Value : (object)phoneNumber.Value);
                    //command.Parameters.AddWithValue("@EmailConfidencial", string.IsNullOrEmpty(emailConfidencial.Value) ? DBNull.Value : (object)emailConfidencial.Value);
                    //command.Parameters.AddWithValue("@ComplainantAdress", string.IsNullOrEmpty(complainantAdress.Value) ? DBNull.Value : (object)complainantAdress.Value);
                    //command.Parameters.AddWithValue("@Comunity", string.IsNullOrEmpty(comunity.Value) ? DBNull.Value : (object)comunity.Value);
                    //command.Parameters.AddWithValue("@Distrit", string.IsNullOrEmpty(distrit.Value) ? DBNull.Value : (object)distrit.Value);


                    command.Parameters.AddWithValue("@ProjectRelated", string.IsNullOrEmpty(projectRelated.Value) ? DBNull.Value : (object)projectRelated.Value);
                    //command.Parameters.AddWithValue("@Method", outro.Checked ? (object)otherMethod.Value : reportingMethod.Value);

                    //// Outros campos
                    //command.Parameters.AddWithValue("@InternalPerson", string.IsNullOrEmpty(internalPerson.Value) ? DBNull.Value : (object)internalPerson.Value);
                    //command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description.Value) ? DBNull.Value : (object)description.Value);


                    //command.Parameters.AddWithValue("@TypeofComplaint", outrotipo.Checked ? (object)outraClassificacao.Value : typeOfComplaint.Value);



                    command.Parameters.AddWithValue("@RedressDate", string.IsNullOrEmpty(redressDateE.Value) ? DBNull.Value : (object)redressDateE.Value);
                    command.Parameters.AddWithValue("@InvestigarionOC", string.IsNullOrEmpty(investigationOutcomeE.Value) ? DBNull.Value : (object)investigationOutcomeE.Value);
                    command.Parameters.AddWithValue("@Outcome", string.IsNullOrEmpty(outcomeE.Value) ? DBNull.Value : (object)outcomeE.Value);
                    command.Parameters.AddWithValue("@ProposedDecision", string.IsNullOrEmpty(proposedDecisionE.Value) ? DBNull.Value : (object)proposedDecisionE.Value);
                    command.Parameters.AddWithValue("@Authority", string.IsNullOrEmpty(authorityE.Value) ? DBNull.Value : (object)authorityE.Value);
                    //command.Parameters.AddWithValue("@ComplainantName", string.IsNullOrEmpty(complainantName.Value) ? DBNull.Value : (object)complainantName.Value);
                    //command.Parameters.AddWithValue("@FinalDecision", string.IsNullOrEmpty(finalDecisionE.Value) ? DBNull.Value : (object)finalDecisionE.Value);
                    command.Parameters.AddWithValue("@RemedialDate", string.IsNullOrEmpty(remedialDateE.Value) ? DBNull.Value : (object)remedialDateE.Value);
                    command.Parameters.AddWithValue("@ActualRemedialDate", string.IsNullOrEmpty(actualremedialDateE.Value) ? DBNull.Value : (object)actualremedialDateE.Value);
                    command.Parameters.AddWithValue("@PersonVerifying", string.IsNullOrEmpty(personVerifyingE.Value) ? DBNull.Value : (object)personVerifyingE.Value);
                    command.Parameters.AddWithValue("@CloseOut", string.IsNullOrEmpty(closeOUtE.Value) ? DBNull.Value : (object)closeOUtE.Value);


                    command.Parameters.AddWithValue("@Escalation", string.IsNullOrEmpty(escalationE.Value) ? DBNull.Value : (object)escalationE.Value);
                    command.Parameters.AddWithValue("@EntityReferred", string.IsNullOrEmpty(entityReferredE.Value) ? DBNull.Value : (object)entityReferredE.Value);
                    command.Parameters.AddWithValue("@Contact", string.IsNullOrEmpty(contactE.Value) ? DBNull.Value : (object)contactE.Value);
                    command.Parameters.AddWithValue("@DateMentioned", string.IsNullOrEmpty(dateMentionedE.Value) ? DBNull.Value : (object)dateMentionedE.Value);
                    //command.Parameters.AddWithValue("@RemedialAction", string.IsNullOrEmpty(remedialActionE.Value) ? DBNull.Value : (object)remedialActionE.Value);
                    command.Parameters.AddWithValue("@CloseDate", string.IsNullOrEmpty(closeDateE.Value) ? DBNull.Value : (object)closeDateE.Value);

                    //faz upload do documento
                    connection.Open();
                    upload();

                    // Salvar o histórico da transição
                    SqlCommand cmdHistorico = new SqlCommand("INSERT INTO HistoricoEstados (QueixaId, Estadoid,DataTransicao,Username) VALUES (@QueixaId, @Estado,@DataMudanca,@Username)", connection);
                    cmdHistorico.Parameters.AddWithValue("@QueixaId", id);
                    cmdHistorico.Parameters.AddWithValue("@Estado", ddlEstadoSecG.SelectedValue);
                    cmdHistorico.Parameters.AddWithValue("@DataMudanca", DateTime.Now);
                    cmdHistorico.Parameters.AddWithValue("@Username", Session["User"]);
                    cmdHistorico.ExecuteNonQuery();

                    // Abrir conexão e executar o comando
                    command.ExecuteNonQuery();
                    CarregarGridView();
                }


            }


        }

        public void datas()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  NameOfComplainant, FileIO, Type from Queixas where ComplaintNr=@Id";
                    cmd.Parameters.AddWithValue("@Id", id_C);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read()) { 
                        DateTime DateComplaint = sdr.GetDateTime(1);
                        string status = sdr["Status"].ToString();
                        string id = sdr["ComplaintNr"].ToString();
                            if(DateTime.Now >= DateComplaint.AddDays(10) && status=="Nivel1" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel2" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel3" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(15) && status=="Nivel4" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel5" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel6" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            }
                        }
                    }

                    con.Close();
                }
            }




        }
        public void compara(SqlConnection con, string id, string status, DateTime DateComplaint)
        {
            //faz calculo da diferenca de dias
            DateTime dataAtual = DateTime.Now;
            TimeSpan diferenca = dataAtual - DateComplaint;
            int diasPassados = diferenca.Days;
            string msg = ("Past " + diasPassados + "days from " + status);
            string query = "update Queixas set Atraso=@Atraso where ComplaintNr=" + id + "";
            using (SqlCommand cmdI = new SqlCommand(query))
            {
                cmdI.Connection = con;
                cmdI.Parameters.AddWithValue("@Atraso", msg);

                con.Open();
                cmdI.ExecuteNonQuery();
                con.Close();
            }
        }


        //carregar as dropdown dos estados
        public void carregarddl(DropDownList dll,string Sec,string Id)
        {
            string query = "SELECT  EstadoId, Estado FROM Estados WHERE Sec='"+ Sec +"' and EstadoId NOT IN ( SELECT EstadoId  FROM HistoricoEstados WHERE QueixaId ='" +Id +"')";
            DataTable Estado = Database.ExecuteQuery(query);

            dll.DataSource = Estado;
            dll.DataTextField = "Estado";
            dll.DataValueField = "EstadoId";
            dll.DataBind();
        }


        //carregar lista de PAPs no dropdown
        public void carregarPAP()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * from PAP";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // Executar a query e ler os resultados
                SqlDataReader dr = cmd.ExecuteReader();
                ddlSearch.DataSource = dr;
                ddlSearch.DataTextField = "Nome";
                ddlSearch.DataValueField = "PAPid";
                ddlSearch.DataBind();

                // Adiciona um item inicial de seleção
                ddlSearch.Items.Insert(0, new ListItem("PAP Name", "0"));
            }




        }

        //qunado e selecionado um PAP deve buscar dados da base de dados e popular os camos da SEC B
        protected void populate(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from PAP where PAPid='" + ddlSearch.SelectedValue.ToString() + "'", connection))
                {

                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        sdr.Read();
                        nameOfComplainant.Value = sdr["Nome"].ToString().Trim();
                        gender.Value = sdr["Gender"].ToString().Trim();
                        phoneNumber.Value = sdr["Phone"].ToString().Trim();
                        //emailConfidencial.Value = sdr["Email"].ToString().Trim();
                        //complainantAdress.Value = sdr["Adress"].ToString().Trim();
                        //comunity.Value = sdr["Community"].ToString().Trim();
                        distrit.Value = sdr["District"].ToString().Trim();



                    }
                }
            }
            string script = "  $(document).ready(function() { $('#novaQueixa').modal('toggle'); });";
            ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);
        }
        
    }    
}