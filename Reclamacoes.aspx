<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Reclamacoes.aspx.cs"  Inherits="SW.Reclamacoes" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-group {
            margin-bottom: 15px;
        }

        label {
            display: block;
            margin-bottom: 5px;
        }

        input[type="text"], input[type="email"], input[type="date"] {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
        }

        .btn {
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
        }

            .btn:hover {
                background-color: #0056b3;
            }

        .accordion-body {
            background-color: #f9f9f9;
        }

        .accordion-item:first-of-type .accordion-button {
            background-color: #f3f8ff;
            color: black;
            font-weight: 600;
            border: 2px solid #1717174a;
            margin-bottom: 5px;
        }

        .accordion-button:focus {
            background-color: #4aa1ff !important
        }

        .gridview-style th, .gridview-style td {
            border: 0px solid white;
            padding: 5px;
            text-align: center;
        }

        .gridview-style th {
            padding: 10px;
        }
    </style>




    <main>




        <div class="container">

            <div class="container-fluid px-4">
                <h1 class="mt-4">Registo de Queixa</h1>

                <div class="card shadow mb-4">
                    <div class="card-header py-2">

                        <div class="btn-toolbar float-right">
                            <%-- botao para abrir modal de nova queixaa --%>
                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#novaQueixa">
                                Registar Queixa
                            </button>
                        </div>
                    </div>
                    <div class="card-body">

                        <asp:GridView CssClass="gridview-style" ID="GridView1" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="factura" runat="server" CausesValidation="False" CommandName="Editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/comment-alt-edit.png" Text=" Selecione " />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="btns_tb" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="select" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="~/Imagens/touchscreen1.png" Text=" Selecione " />

                                    </ItemTemplate>
                                    <ItemStyle CssClass="btns_tb" />
                                </asp:TemplateField>--%>

                                <asp:BoundField DataField="ComplaintNr" HeaderText="ComplaintNr" ReadOnly="True" SortExpression="ComplaintNr" />
                                <asp:BoundField DataField="ComplaintCode" HeaderText="ComplaintCode" ReadOnly="True" SortExpression="ComplaintCode" />
                                <asp:BoundField DataField="DateComplaint" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Complaint Date" ReadOnly="True" SortExpression="DateComplaint" />
                                <asp:BoundField DataField="PAPid" HeaderText="PAP ID" ReadOnly="True" SortExpression="PAPid" />
                                <asp:BoundField DataField="NameOfComplainant" HeaderText="Name Of Complainant" ReadOnly="True" SortExpression="NameOfComplainant" />
                                <asp:BoundField DataField="ProjectRelated" HeaderText="Project Related" ReadOnly="True" SortExpression="ProjectRelated" />
                                <asp:BoundField DataField="TypeofComplaint" HeaderText="Type" ReadOnly="True" SortExpression="TypeofComplaint" />
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />


                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="download" runat="server" CausesValidation="False" CommandName="Download" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/arrow-small-down.png" Text=" Selecione " />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="btns_tb" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>



                    </div>
                </div>
            </div>
        </div>



        <%-- Modal de nova queixa --%>
        <div class="modal fade" id="novaQueixa" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="exampleModalLabel">Registar Queixa</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <label for="FullName">Complaint/Suggestion:</label>

                        <select name="confidentiality" runat="server" id="tipo">
                            <option value=" " selected></option>
                            <option value="Complaint">Complaint</option>
                            <option value="Suggestion">Suggestion</option>
                        </select>

                        <div class="accordion" id="accordionPanelsStayOpenExample">
                            <div class="accordion-item">
                                <h2 class="accordion-header">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                                        Section A:  Identification of Complainant
                                    </button>
                                </h2>
                                <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show">
                                    <div class="accordion-body">
                                        <div class="form-group">
                                            <%--                                            <label for="FullName">Complaint No :</label>
                                            <input id="complaintNr" runat="server" class="form-control">--%>
                                            <%--<asp:RequiredFieldValidator ID="rfvCasoNr" runat="server" ControlToValidate="complaintNr" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtData">Date of the Complaint(dd/mm/yyyy):</label>
                                            <input id="dateComplaint" runat="server" type="date" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="dateComplaint" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="Email">PAP Identification No: </label>
                                            <%--<input id="PAPid" runat="server" class="form-control">--%>
                                            <asp:DropDownList ID="ddlSearch" CssClass="form-control" Style="width: 300px" runat="server">
                                            </asp:DropDownList>
                                            <input type="checkbox" name="is"  onchange="PapRelated()" id="CheckboxPAP" />
                                            <label for="is" style="display:inline-block">Not PAP </label>
                                            <select name="parentesco" runat="server" id="parentesco" CssClass="form-control" Style="width: 300px">
                                                <option value=" " selected></option>
                                                <option value="Yes">...</option>
                                                <option value="No">...</option>
                                            </select>
                                            <asp:Button ID="Button1" runat="server" Text="Poulate" OnClick="populate" />
                                        </div>

                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                            Section B: Geotechnical Location of Complaint/Suggestion
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse">

                                        <div class="form-group">
                                            <label for="confidentiality">Confidentiality (Yes/No):</label><%--Radio--%>
                                            <select name="confidentiality" runat="server" id="confidentiality" onchange="toggleInputs()">
                                                <option value=" " selected></option>
                                                <option value="Yes">Yes</option>
                                                <option value="No">No</option>
                                            </select>
                                            <%--<asp:RequiredFieldValidator ID="rfvConfidentiality" runat="server" ControlToValidate="confidentiality" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label for="Provincia">Name of the Complainant: </label>
                                            <input id="nameOfComplainant" runat="server" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvnoc"  runat="server" ControlToValidate="nameOfComplainant" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="gender">Gender:</label>

                                            <select name="gender" class="form-control" runat="server" id="gender">
                                                <option value=" " selected></option>
                                                <option value="Male">Male</option>
                                                <option value="Female">Female</option>
                                            </select>
                                            <%--<asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="gender" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label for="phoneNumber">Phone Number:</label>
                                            <input id="phoneNumber" runat="server" type="text" maxlength="9" class="form-control" pattern="[0-9]*" placeholder="Ex: 820000000" onblur="validarNumero(this)" />
                                            <%--<asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="phoneNumber" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label for="email">E-mail:</label>
                                            <input id="emailConfidencial" runat="server" class="form-control" type="email" />
                                            <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="emailConfidencial" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>



                                        <div class="form-group">
                                            <label for="Provincia">Complainant Address:</label>
                                            <input id="complainantAdress" runat="server" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvAdress" runat="server" ControlToValidate="complainantAdress" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="comunity">Community/Village:</label>
                                            <select name="comunity" class="form-control" runat="server" id="comunity">
                                                <option value=" " selected></option>
                                                <option value="...">...</option>
                                                <option value="...">...</option>
                                            </select>
                                            <%--<asp:RequiredFieldValidator ID="rfvComunidade" runat="server" ControlToValidate="comunity" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="Distrito">Distrit:</label>
                                            <input id="distrit" runat="server" value="Cidade de Mucuba" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvDistrito" runat="server" ControlToValidate="distrit" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="relacionado">Project Related Complaint/Suggestion</label>
                                            <div>

                                                <select name="pr" runat="server" clientidmode="Static" id="projectRelated">
                                                    <option value="YES">YES</option>
                                                    <option value="NOT APPLICABLE">NO</option>
                                                </select>

                                            </div>


                                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDistrito" ErrorMessage="Gender is required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="relacionado">Grievance Reporting Method Used(select relevant)</label>
                                            <div style="display: flex; gap: 10px">

                                                <select name="rm" runat="server" id="reportingMethod">
                                                    <option value=" " selected></option>
                                                    <option value="Green Line">Green Line</option>
                                                    <option value="Email">Email</option>
                                                    <option value="SMS">SMS</option>
                                                    <option value="CRC">CRC</option>
                                                    <option value="Complaint Box">Complaint Box</option>
                                                </select>

                                                <div>
                                                    <input type="checkbox" onchange="desabilitaLista()" runat="server" value="Outro" id="outro" name="outro" />
                                                    <label for="outro">Other</label>
                                                    <input id="otherMethod" runat="server" class="form-control">
                                                </div>
                                                <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDistrito" ErrorMessage="Gender is required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="accordion-item">
                                        <h2 class="accordion-header">
                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseThree" aria-expanded="false" aria-controls="panelsStayOpen-collapseThree">
                                                Section C: Brief Description of Complaint/Suggestion
                                            </button>
                                        </h2>
                                        <div id="panelsStayOpen-collapseThree" class="accordion-collapse collapse">
                                            <div class="accordion-body">

                                                <div class="form-group">
                                                    <label for="pessoaRegistou">Name of the person receiving and recording the grievance (Internal personnel)</label>
                                                    <input type="text" id="internalPerson" class="form-control" runat="server" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvpessoaRegistou" runat="server" ControlToValidate="internalPerson" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group">
                                                    <label for="descricao">Description of the Complaint:</label>
                                                    <textarea id="description" runat="server" class="form-control" name="descricao" rows="4"></textarea>
                                                    <%--<asp:RequiredFieldValidator ID="rfvdescricao" runat="server" ControlToValidate="description" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                </div>




                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseFour" aria-expanded="false" aria-controls="panelsStayOpen-collapseFour">
                                            Section D: Classification of Complaint (Tick Relevant)
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseFour" class="accordion-collapse collapse">
                                        <div class="accordion-body">
                                            <div class="form-group">
                                                <div style="display: flex;">

                                                    <div>
                                                        <h5>TYPE OF COMPLAINT/SUGGESTION</h5>

                                                    </div>
                                                    <select name="cars" runat="server" id="typeOfComplaint">
                                                        <option value=" " selected></option>
                                                        <option value="SOCIAL">SOCIAL</option>
                                                        <option value="ENVIRONMENTAL">ENVIRONMENTAL</option>
                                                        <option value="LABOUR">LABOUR</option>
                                                        <option value="esettlement Related">Resettlement Related</option>
                                                        <option value="GBV/TIP/SEAH/SH">GBV/TIP/SEAH/SH</option>
                                                    </select>


                                                </div>
                                                <div style="display: flex;">
                                                    <div>
                                                        <input type="checkbox" onchange="desabilitaListaSecD()" runat="server" value="Outro" id="outrotipo" name="outro" />
                                                        <h5>OTHER</h5>
                                                    </div>
                                                    <div>
                                                        <div class="form-group">
                                                            <label for="Comun">(Write Below):</label>
                                                            <input id="outraClassificacao" runat="server" class="form-control">
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>



                                            <%--                                            <div class="accordion-item">
                                                <h2 class="accordion-header">
                                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFive" aria-expanded="true" aria-controls="collapseFive">
                                                        PART 2: Confidentiality Request
                                                    </button>
                                                </h2>
                                                <div id="collapseFive" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                                                    <div class="accordion-body">



                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <%--   <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseSix" aria-expanded="false" aria-controls="panelsStayOpen-collapseSix">
                                            Section G: GRIEVANCE REDRESSAL RESPONSE
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseSix" class="accordion-collapse collapse">
                                        <div class="accordion-body">

                                            <div class="form-group">
                                                <label for="redressDate">Date of Redress/Investigation(dd/mm/yyyy):</label>
                                                <input id="redressDate" runat="server" class="form-control" type="date" />
                                            </div>
                                            <div class="form-group">
                                                <label for="proposedDecision">Investigation Outcome:</label>
                                                <textarea id="proposedDecision" runat="server" class="form-control"></textarea>
                                            </div>
                                            <div class="form-group">
                                                <label for="outcome">Complainant Accepts the Outcome (select relevant option)</label>
                                                <select name="outcome" runat="server" id="outcome">
                                                    <option value=" " selected></option>
                                                    <option value="Yes">Yes</option>
                                                    <option value="No">No</option>
                                                </select>

                                            </div>
                                            <%--  <div class="form-group">
                                                <label for="complainantName">Name of Complainant (where applicable):</label>
                                                <input id="complainantName" runat="server" class="form-control" type="text" />
                                                <asp:RequiredFieldValidator ID="rfvComplainantName" runat="server" ControlToValidate="complainantName" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                <label for="complainantSignature">Signature of the Complainant:</label>
                                                <input id="complainantSignature" runat="server" class="form-control" type="text" />
                                            </div>
                                            <div class="form-group">
                                                <label for="grievanceOfficerSignature">Signature of the Grievance Officer:</label>
                                                <input id="grievanceOfficerSignature" runat="server" class="form-control" type="text" />
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Agreed Remedial Action (please give full details):</label>
                                                <textarea id="finalDecision" runat="server" class="form-control"></textarea>
                                            </div>

                                            <div class="form-group">
                                                <label for="finalDecision">Remedial Action Implementing Authority:</label>
                                                <input id="authority" runat="server" class="form-control">
                                            </div>

                                            <div class="form-group">
                                                <label for="finalDecision">Remedial Action Targeted Completed Date: (dd/mm/yyyy)</label>
                                                <input id="remedialDate" type="date" runat="server" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Actual Date of Completion:(dd/mm/yyyy)</label>
                                                <input id="actualremedialDate" type="date" runat="server" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Person Verifying the Outcome: </label>
                                                <input id="personVerifying" runat="server" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Close out date:(dd/mm/yyyy)</label>
                                                <input id="closeOUt" type="date" runat="server" class="form-control">
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseSeven" aria-expanded="false" aria-controls="panelsStayOpen-collapseSeven">
                                            Section H: Referrals
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseSeven" class="accordion-collapse collapse">
                                        <div class="accordion-body">
                                            <div class="form-group">
                                                <label for="escalation">Escalation to the next level: (Yes/No)</label>
                                                <select name="escalation" onchange="toggleInputsSecH()" runat="server" id="escalation">
                                                    <option value=" "></option>
                                                    <option value="Yes">Yes</option>
                                                    <option value="No">No</option>
                                                </select>

                                            </div>
                                            <div class="form-group">
                                                <label for="entityReferred">Entity Referred to:</label>
                                                <input id="entityReferred" runat="server" class="form-control" type="text" />
                                            </div>
                                            <div class="form-group">
                                                <label for="contact">Contact and Name:</label>
                                                <input id="contact" runat="server" class="form-control" type="text" />
                                            </div>
                                            <div class="form-group">
                                                <label for="dateMentioned">Date Referred:(dd/mm/yyyy):</label>
                                                <input id="dateMentioned" runat="server" class="form-control" type="date" />
                                            </div>
                                            <div class="form-group">
                                                <label for="dateMentioned">Remedial action taken to resolve the case:</label>
                                                <input id="remedialAction" runat="server" class="form-control" type="date" />
                                            </div>

                                            <div class="form-group">
                                                <label for="dateMentioned">Close out date:(dd/mm/yyyy)</label>
                                                <input id="closeDate" runat="server" class="form-control" type="date" />
                                            </div>




                                        </div>
                                    </div> 
                                </div>--%>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="btnSubmit" runat="server" Text="Register" CssClass="btn" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>


        <%--modal editar queixa--%>
        <div class="modal fade" id="editarQueixa" data-bs-backdrop="static" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="">Atualizar Queixa</h1>
                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-pr close" OnClick="exitModal_Click" Text="&times;" />
                    </div>
                    <div class="modal-body">
                        <label for="FullName">Complaint/Suggestion:</label>

                        <select name="confidentiality" runat="server" id="tipoE">
                            <option value=" " selected></option>
                            <option value="Complaint">Complaint</option>
                            <option value="Suggestion">Suggestion</option>
                        </select>

                        <div class="accordion" id="accordionPanelsStayOpenExample1">
                            <div class="accordion-item">
                                <h2 class="accordion-header">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseEight" aria-expanded="true" aria-controls="panelsStayOpen-collapseEight">
                                        Section A:  Identification of Complainant
                                    </button>
                                </h2>
                                <div id="panelsStayOpen-collapseEight" class="accordion-collapse collapse show">
                                    <div class="accordion-body">
                                        <div class="form-group">
                                            <label for="FullName">Complaint No :</label>
                                            <input style="pointer-events: none;" id="complaintNrE" runat="server" class="form-control">
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvCasoNr" runat="server" ControlToValidate="complaintNr" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="txtData">Date of the Complaint(dd/mm/yyyy):</label>
                                            <input id="dateComplaintE" runat="server" type="date" class="form-control">
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="dateComplaint" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="Email">PAP Identification No: </label>
                                            <input id="PAPidE" runat="server" class="form-control">
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvIdReclamacao" runat="server" ControlToValidate="PAPid" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            <%--                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+@\w+\.\w+" ErrorMessage="Enter a valid email!" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                        </div>

                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseNine" aria-expanded="false" aria-controls="panelsStayOpen-collapseNine">
                                            Section B: Geotechnical Location of Complaint/Suggestion
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseNine" class="accordion-collapse collapse">

                                        <div class="form-group">
                                            <label for="confidentiality">Confidentiality (Yes/No):</label><%--Radio--%>
                                            <select name="confidentiality" runat="server" id="confidentialityE" onchange="toggleInputs()">
                                                <option value=" " selected></option>
                                                <option value="Yes">Yes</option>
                                                <option value="No">No</option>
                                            </select>
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvConfidentiality" runat="server" ControlToValidate="confidentiality" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label for="Provincia">Name of the Complainant: </label>
                                            <input id="nameOfComplainantE" runat="server" class="form-control">
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvnoc" runat="server" ControlToValidate="nameOfComplainant" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="gender">Gender:</label>
                                            <input id="genderE" runat="server" class="form-control" type="text" />
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="gender" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label for="phoneNumber">Phone Number:</label>
                                            <input id="phoneNumberE" runat="server" class="form-control" type="tel" />
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="phoneNumber" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label for="email">E-mail:</label>
                                            <input id="emailConfidencialE" runat="server" class="form-control" type="email" />
                                            <%--                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="emailConfidencial" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>



                                        <div class="form-group">
                                            <label for="Provincia">Complainant Address:</label>
                                            <input id="complainantAdressE" runat="server" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvAdress" runat="server" ControlToValidate="complainantAdress" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="Comun">Community/Village:</label>
                                            <input id="comunityE" runat="server" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvComunidade" runat="server" ControlToValidate="comunity" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="Distrito">Distrit:</label>
                                            <input id="distritE" runat="server" class="form-control">
                                            <%--<asp:RequiredFieldValidator ID="rfvDistrito" runat="server" ControlToValidate="distrit" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="relacionado">Project Related Complaint/Suggestion</label>
                                            <div>

                                                <select name="pr" runat="server" class="form-control" id="projectRelatedE">
                                                    <option value=" " selected></option>
                                                    <option value="Yes">Yes</option>
                                                    <option value="No">No</option>
                                                </select>

                                            </div>


                                            <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDistrito" ErrorMessage="Gender is required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group">
                                            <label for="relacionado">Grievance Reporting Method Used(select relevant)</label>
                                            <div style="display: flex; gap: 10px">

                                                <select name="rm" runat="server" id="reportingMethodE">
                                                    <option value=" " selected></option>
                                                    <option value="Green Line">Green Line</option>
                                                    <option value="Email">Email</option>
                                                    <option value="SMS">SMS</option>
                                                    <option value="CRC">CRC</option>
                                                    <option value="Complaint Box">Complaint Box</option>
                                                </select>

                                                <div>
                                                    <input type="checkbox" onchange="desabilitaLista()" runat="server" value="Outro" id="outroE" name="outro" />
                                                    <label for="outro">Other</label>
                                                    <input id="otherMethodE" runat="server" class="form-control">
                                                </div>
                                                <%--                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDistrito" ErrorMessage="Gender is required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="accordion-item">
                                        <h2 class="accordion-header">
                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTen" aria-expanded="false" aria-controls="panelsStayOpen-collapseTen">
                                                Section C: Brief Description of Complaint/Suggestion
                                            </button>
                                        </h2>
                                        <div id="panelsStayOpen-collapseTen" class="accordion-collapse collapse">
                                            <div class="accordion-body">

                                                <div class="form-group">
                                                    <label for="pessoaRegistou">Name of the person receiving and recording the grievance (Internal personnel)</label>
                                                    <input type="text" id="internalPersonE" class="form-control" runat="server" />
                                                    <%--                                                    <asp:RequiredFieldValidator ID="rfvpessoaRegistou" runat="server" ControlToValidate="internalPerson" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group">
                                                    <label for="descricao">Description of the Complaint:</label>
                                                    <textarea id="descriptionE" runat="server" class="form-control" name="descricao" rows="4"></textarea>
                                                    <%--<asp:RequiredFieldValidator ID="rfvdescricao" runat="server" ControlToValidate="description" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                </div>




                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseEleven" aria-expanded="false" aria-controls="panelsStayOpen-collapseEleven">
                                            Section D: Classification of Complaint (Tick Relevant)
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseEleven" class="accordion-collapse collapse">
                                        <div class="accordion-body">
                                            <div class="form-group">
                                                <div style="display: flex;">

                                                    <div>

                                                        <h5>TYPE OF COMPLAINT/SUGGESTION</h5>

                                                    </div>



                                                    <select name="cars" runat="server" id="typeOfComplaintE">
                                                        <option value=" " selected></option>
                                                        <option value="SOCIAL">SOCIAL</option>
                                                        <option value="ENVIRONMENTAL">ENVIRONMENTAL</option>
                                                        <option value="LABOUR">LABOUR</option>
                                                        <option value="esettlement Related">Resettlement Related</option>
                                                        <option value="GBV/TIP/SEAH/SH">GBV/TIP/SEAH/SH</option>
                                                    </select>


                                                </div>
                                                <div style="display: flex;">
                                                    <div>
                                                        <input type="checkbox" onchange="desabilitaListaSecD()" runat="server" value="Outro" id="outrotipoE" name="outro" />
                                                        <h5>OTHER</h5>
                                                    </div>
                                                    <div>
                                                        <div class="form-group">
                                                            <label for="Comun">(Write Below):</label>
                                                            <input id="outraClassificacaoE" runat="server" class="form-control">
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>



                                            <%--                                            <div class="accordion-item">
                                                <h2 class="accordion-header">
                                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFive" aria-expanded="true" aria-controls="collapseFive">
                                                        PART 2: Confidentiality Request
                                                    </button>
                                                </h2>
                                                <div id="collapseFive" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                                                    <div class="accordion-body">



                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwelve" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwelve">
                                            Section G: GRIEVANCE REDRESSAL RESPONSE
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseTwelve" class="accordion-collapse collapse">
                                        <div class="accordion-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" AssociatedControlID="ddlEstadoSecG">State:</asp:Label>
                                                <asp:DropDownList ID="ddlEstadoSecG" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="redressDate">Date of Redress/Investigation(dd/mm/yyyy):</label>
                                                <input id="redressDateE" runat="server" class="form-control" type="date" />
                                                <%--<asp:RequiredFieldValidator ID="rfvRedressDateE" runat="server" ControlToValidate="redressDateE" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="proposedDecision">Investigation Outcome:</label>
                                                <textarea id="proposedDecisionE" runat="server" class="form-control"></textarea>
                                                <br />

                                                <%--<asp:RequiredFieldValidator ID="rfvProposedDecision" runat="server" ControlToValidate="proposedDecision" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="outcome">Complainant Accepts the Outcome (select relevant option)</label><%--Radio--%>
                                                <select name="pr" runat="server" onchange="desabilitaListaSecGH()" clientidmode="Static" id="outcomeE">
                                                    <option value=" " selected></option>
                                                    <option value="Yes">Yes</option>
                                                    <option value="No">No</option>
                                                </select>



                                                <%--<asp:RequiredFieldValidator ID="rfvOutcome" runat="server" ControlToValidate="outcome" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <%--  <div class="form-group">
                                                <label for="complainantName">Name of Complainant (where applicable):</label>
                                                <input id="complainantName" runat="server" class="form-control" type="text" />
                                                <asp:RequiredFieldValidator ID="rfvComplainantName" runat="server" ControlToValidate="complainantName" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                <label for="complainantSignature">Signature of the Complainant:</label>
                                                <input id="complainantSignature" runat="server" class="form-control" type="text" />
                                            </div>
                                            <div class="form-group">
                                                <label for="grievanceOfficerSignature">Signature of the Grievance Officer:</label>
                                                <input id="grievanceOfficerSignature" runat="server" class="form-control" type="text" />
                                            </div>--%>
                                            <div class="form-group">
                                                <label for="finalDecision">Agreed Remedial Action (please give full details):</label>
                                                <textarea id="finalDecisionE" runat="server" class="form-control"></textarea>
                                                <%--<asp:RequiredFieldValidator ID="rfvFinalDecision" runat="server" ControlToValidate="finalDecision" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                <h4>Ficheiro não pode exceder 2MB</h4>
                                                <asp:FileUpload ID="FileUploadIO" runat="server" />
                                                <asp:Button runat="server" Text="Upload" OnClick="upload_Click" />

                                            </div>

                                            <div class="form-group">
                                                <label for="finalDecision">Remedial Action Implementing Authority:</label>
                                                <input id="authorityE" runat="server" class="form-control">
                                                <%--<asp:RequiredFieldValidator ID="rfvAuto" runat="server" ControlToValidate="authority" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group">
                                                <label for="finalDecision">Remedial Action Targeted Completed Date: (dd/mm/yyyy)</label>
                                                <input id="remedialDateE" type="date" runat="server" class="form-control">
                                                <%--<asp:RequiredFieldValidator ID="rfvremedialDate" runat="server" ControlToValidate="remedialDate" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Actual Date of Completion:(dd/mm/yyyy)</label>
                                                <input id="actualremedialDateE" type="date" runat="server" class="form-control">
                                                <%--<asp:RequiredFieldValidator ID="rfvactualremedialDate" runat="server" ControlToValidate="actualremedialDate" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Person Verifying the Outcome: </label>
                                                <input id="personVerifyingE" runat="server" class="form-control">
                                                <%--<asp:RequiredFieldValidator ID="rfvpersonVerifying" runat="server" ControlToValidate="personVerifying" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="finalDecision">Close out date:(dd/mm/yyyy)</label>
                                                <input id="closeOUtE" type="date" runat="server" class="form-control">
                                                <%--<asp:RequiredFieldValidator ID="rfvcloseOUt" runat="server" ControlToValidate="closeOUt" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseThirteen" aria-expanded="false" aria-controls="panelsStayOpen-collapseThirteen">
                                            Section H: Referrals
                                        </button>
                                    </h2>
                                    <div id="panelsStayOpen-collapseThirteen" class="accordion-collapse collapse">
                                        <div class="accordion-body">
                                            <div class="form-group">
                                                <label for="escalation">Escalation to the next level: (Yes/No)</label>
                                                <select name="escalation" onchange="toggleInputsSecH()" runat="server" id="escalationE">
                                                    <option value=" "></option>
                                                    <option value="Yes">Yes</option>
                                                    <option value="No">No</option>
                                                </select>

                                                <%--<asp:RequiredFieldValidator ID="rfvEscalation" runat="server" ControlToValidate="escalation" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="entityReferred">Entity Referred to:</label>
                                                <input id="entityReferredE" runat="server" class="form-control" type="text" />
                                                <%--<asp:RequiredFieldValidator ID="rfventityReferred" runat="server" ControlToValidate="entityReferred" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="contact">Contact and Name:</label>
                                                <input id="contactE" runat="server" class="form-control" type="text" />
                                            </div>
                                            <div class="form-group">
                                                <label for="dateMentioned">Date Referred:(dd/mm/yyyy):</label>
                                                <input id="dateMentionedE" runat="server" class="form-control" type="date" />
                                                <%--<asp:RequiredFieldValidator ID="rfvdateMentioned" runat="server" ControlToValidate="dateMentioned" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group">
                                                <label for="dateMentioned">Remedial action taken to resolve the case:</label>
                                                <input id="remedialActionE" runat="server" class="form-control" type="text" />
                                                <%--<asp:RequiredFieldValidator ID="rfvremedialAction" runat="server" ControlToValidate="remedialAction" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group">
                                                <label for="dateMentioned">Close out date:(dd/mm/yyyy)</label>
                                                <input id="closeDateE" runat="server" class="form-control" type="date" />
                                                <%--<asp:RequiredFieldValidator ID="rfvcloseDate" runat="server" ControlToValidate="closeDate" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>--%>
                        <asp:Button ID="btnAtualizar" runat="server" Text="Update" CssClass="btn" OnClick="btnAtualizar_Click" />
                    </div>
                </div>
            </div>
        </div>


<button type="button" id="meuBotao" onclick="chamarMetodo()">Clique Aqui</button>
    <div id="resultado"></div>      

        


    </main>
    <script>
        $(document).ready(function () {
            // Reset modal state to ensure it doesn't open automatically
            $('#editarQueixa').modal('hide');
            document.getElementById('<%= otherMethod.ClientID %>').disabled = true;
            document.getElementById('<%= outraClassificacao.ClientID %>').disabled = true;
            desabilitaListaEdicao();
            desabilitaListaSecGH()
            toggleInputsSecH()
            PapRelated()
            //const inputs = document.querySelectorAll('input');

            //// Itera sobre os elementos selecionados e imprime o ID ou valor de cada um
            //inputs.forEach(input => {
            //    input.disabled = true

            //});
        });



        //funcao para desabilitar campos confidencialidade
        function toggleInputs() {
            const select = document.getElementById('<%= confidentiality.ClientID %>');
            const input1 = document.getElementById('<%= nameOfComplainant.ClientID %>');
            const input2 = document.getElementById('<%= gender.ClientID %>');
            const input3 = document.getElementById('<%= phoneNumber.ClientID %>');
            const input4 = document.getElementById('<%= emailConfidencial.ClientID %>');
            const input5 = document.getElementById('<%= complainantAdress.ClientID %>');
            const input6 = document.getElementById('<%= comunity.ClientID %>');
            const input7 = document.getElementById('<%= distrit.ClientID %>');


            // Desabilita os inputs se a opção 2 for selecionada
            if (select.value === 'Yes') {
                input1.disabled = true;
                input2.disabled = true;
                input3.disabled = true;
                input4.disabled = true;
                input5.disabled = true;
                input6.disabled = true;
                input7.disabled = true;
            } else {
                input1.disabled = false;
                input2.disabled = false;
                input3.disabled = false;
                input4.disabled = false;
                input5.disabled = false;
                input6.disabled = false;
                input7.disabled = false;
            }
        }

        //desabilitar inputs SecH
        function toggleInputsSecH() {
            const select = document.getElementById('<%= escalationE.ClientID %>');
            const input1 = document.getElementById('<%= contactE.ClientID %>');
            const input2 = document.getElementById('<%= dateMentionedE.ClientID %>');
            const input3 = document.getElementById('<%= remedialActionE.ClientID %>');
            const input4 = document.getElementById('<%= closeDateE.ClientID %>');
            const input5 = document.getElementById('<%= entityReferredE.ClientID %>');



            // Desabilita os inputs se a opção 2 for selecionada
            if (select.value === 'No' || select.value === ' ') {
                input1.disabled = true;
                input2.disabled = true;
                input3.disabled = true;
                input4.disabled = true;
                input5.disabled = true;

            } else {
                input1.disabled = false;
                input2.disabled = false;
                input3.disabled = false;
                input4.disabled = false;
                input5.disabled = false;

            }
        }

        //desabilitar combobox de metodo de reporte
        function desabilitaLista() {
            const other = document.getElementById('<%= outro.ClientID %>')
            const text = document.getElementById('<%= otherMethod.ClientID %>')
            const list = document.getElementById('<%= reportingMethod.ClientID %>')

            if (other.checked) {
                list.disabled = true;
                text.disabled = false;
            } else {
                list.disabled = false;
                text.disabled = true;

            }
        }
        //desabilitar combobox SEC D part 1
        function desabilitaListaSecD() {
            const other = document.getElementById('<%= outrotipo.ClientID %>')
            const text = document.getElementById('<%= outraClassificacao.ClientID %>')
            const list = document.getElementById('<%= typeOfComplaint.ClientID %>')

            if (other.checked) {
                list.disabled = true;
                text.disabled = false;
            } else {
                list.disabled = false;
                text.disabled = true;

            }
        }

        //SEC G e SECH
        function desabilitaListaSecGH() {
            const select = document.getElementById('<%= outcomeE.ClientID %>');
            const input1 = document.getElementById('<%= proposedDecisionE.ClientID %>');
            const input2 = document.getElementById('<%= authorityE.ClientID %>');
            const input3 = document.getElementById('<%= remedialDateE.ClientID %>');
            const input4 = document.getElementById('<%= actualremedialDateE.ClientID %>');
            const input5 = document.getElementById('<%= personVerifyingE.ClientID %>');
            const input6 = document.getElementById('<%= closeOUtE.ClientID %>');
            const input7 = document.getElementById('<%= finalDecisionE.ClientID %>');


    <%--            const input7 = document.getElementById('<%= finalDecisionE.ClientID %>');--%>


            // Desabilita os inputs se a opção 2 for selecionada
            if (select.value === 'No') {
                document.getElementById('<%= escalationE.ClientID %>').value = 'Yes';
                toggleInputsSecH()
                document.getElementById('<%= escalationE.ClientID %>').disabled = false

            }
            if (select.value === 'No' || select.value === ' ') {
                input1.disabled = true;
                input2.disabled = true;
                input3.disabled = true;
                input4.disabled = true;
                input5.disabled = true;
                input6.disabled = true;
                input7.disabled = true;
            } else {
                input1.disabled = false;
                input2.disabled = false;
                input3.disabled = false;
                input4.disabled = false;
                input5.disabled = false;
                input6.disabled = false;
                input7.disabled = false;
                document.getElementById('<%= escalationE.ClientID %>').disabled = true
                document.getElementById('<%= escalationE.ClientID %>').value = 'No';
                toggleInputsSecH()
            }
        }

        //funcao pra desabilitar campos na edicao
        function desabilitaListaEdicao() {
            document.getElementById('<%= tipoE.ClientID %>').disabled = true
            ////////document.getElementById('<%= complaintNrE.ClientID %>').disabled = true
            document.getElementById('<%= dateComplaintE.ClientID %>').disabled = true
            document.getElementById('<%= PAPidE.ClientID %>').disabled = true


            document.getElementById('<%= confidentialityE.ClientID %>').disabled = true
            document.getElementById('<%= nameOfComplainantE.ClientID %>').disabled = true
            document.getElementById('<%= genderE.ClientID %>').disabled = true
            document.getElementById('<%= phoneNumberE.ClientID %>').disabled = true
            document.getElementById('<%= emailConfidencialE.ClientID %>').disabled = true
            document.getElementById('<%= complainantAdressE.ClientID %>').disabled = true
            document.getElementById('<%= comunityE.ClientID %>').disabled = true
            document.getElementById('<%= distritE.ClientID %>').disabled = true
            document.getElementById('<%= projectRelatedE.ClientID %>').disabled = true
            document.getElementById('<%= reportingMethodE.ClientID %>').disabled = true
            document.getElementById('<%= outroE.ClientID %>').disabled = true
            document.getElementById('<%= otherMethodE.ClientID %>').disabled = true


            document.getElementById('<%= internalPersonE.ClientID %>').disabled = true
            document.getElementById('<%= descriptionE.ClientID %>').disabled = true


            document.getElementById('<%= typeOfComplaintE.ClientID %>').disabled = true
            document.getElementById('<%= outrotipoE.ClientID %>').disabled = true
            document.getElementById('<%= outraClassificacaoE.ClientID %>').disabled = true


        }


        // Função para validar o número
        function validarNumero(element) {
            // Obtém o valor do campo de input
            let numero = element.value;

            // Expressão regular para validar o prefixo e comprimento
            // Prefixos válidos: 82, 83, 84, 85, 86, 87 e 9 dígitos no total
            let regex = /^(82|83|84|85|86|87)\d{7}$/;

            // Verifica se o valor corresponde à expressão regular
            if (!regex.test(numero)) {
                // Exibe uma mensagem de erro e limpa o valor inválido
                alert("Número inválido! Insira um número com até 9 dígitos começando com 82, 83, 84, 85, 86 ou 87.");
                element.value = ""; // Limpa o campo de entrada
                return false;
            }

            // Número válido
            return true;
        }

        ///codigo para o select do PAPid
        $(document).ready(function () {
            $('#<%= ddlSearch.ClientID %>').select2({
                placeholder: "Digite para pesquisar queixas...",
                minimumInputLength: 1, // Começa a buscar com 1 caractere
                dropdownParent: $('#novaQueixa')

            });
        });

        function PapRelated() {
            const ddl = document.getElementById('<%=ddlSearch.ClientID%>')
            const ddlParent = document.getElementById('<%=parentesco.ClientID%>')
            const ckbx = document.getElementById('CheckboxPAP')

            if (ckbx.checked) {
                ddl.disabled = true;
                ddlParent.disabled = false;
            } else {
                ddl.disabled = false;
                ddlParent.disabled = true;
            }

        }


        //PageMethods.SeuMetodo("valor do parâmetro", onSuccess, onFailure);

        //function onSuccess(result) {
        //    console.log("Método chamado com sucesso. Resultado: " + result);
        //}

        //function onFailure(error) {
        //    console.error("Erro ao chamar o método: " + error.get_message());
        //}

        //function chamarMetodo() {
        //    var parametro = "Teste";
        //    PageMethods.SW_Reclamacoes.MetodoNoCodBehind(parametro, onSuccess, onFailure);
        //}

        //function onSuccess(result) {
        //    document.getElementById('resultado').innerHTML = result;
        //}

        //function onFailure(error) {
        //    alert("Erro: " + error.get_message());
        //}
    </script>

</asp:Content>
