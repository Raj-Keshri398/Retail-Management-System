<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register New UserID.aspx.cs" Inherits="Register_New_UserID" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Here New UserID</title>
    <style type="text/css">
        body {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            background-color: #f0f0f0;
        }

        .container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: auto;
            width: 450px;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ccc;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 12px;
        }

        .title {
            margin: 0;
            background-color: #000;
            width: 100%;
            box-sizing: border-box;
            color: #fff;
            padding: 10px;
            font-size: 24px;
            text-align: center;
            border-radius: 10px 10px 0 0;
        }

        .box {
            width: 100%;
            padding: 20px;
            box-sizing: border-box;
        }

        .fields {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }

        .label {
            width: 150px;
            font-size: 15px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        }

        .ml-10 {
            flex: 1;
            margin-left: 10px;
        }

        .buttonContainer {
            display: flex;
            padding: 5px 0;
        }

        .btn {
            margin: 0;
            padding: 8px 24px;
            background-color: #6fa1a1;
            border: 2px solid #6fa1a1;
            border-radius: 8px;
            cursor: pointer;
            color: #fff;
            text-align: center;
            text-decoration: none;
            font-size: 16px;
            margin-left: 40px;
            margin-right: 90px;
        }

        .btn:hover {
            background-color: #578989;
            border-color: #578989;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h3 class="title">Registration</h3>
            <div class="box">
                <div class="fields">
                    <asp:Label ID="Label1" runat="server" Text="User ID" CssClass="label"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="ml-10" Height="24px" Width="200px"></asp:TextBox>
                </div>
                <div class="fields">
                    <asp:Label ID="Label2" runat="server" Text="Password" CssClass="label"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="ml-10" Height="24px" Width="200px" TextMode="Password"></asp:TextBox>
                </div>
                <div class="fields">
                    <asp:Label ID="Label3" runat="server" Text="Confirm Password" CssClass="label"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="ml-10" Height="24px" Width="200px" TextMode="Password"></asp:TextBox>
                </div>
                <div class="fields">
                    <asp:Label ID="Label4" runat="server" Text="Counter" CssClass="label"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="ml-10" Height="24px" Width="200px" TextMode="Password"></asp:TextBox>
                </div>
                
                <div class="buttonContainer">
                    <asp:Button ID="Button1" runat="server" Text="Clear" CssClass="btn" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Register" CssClass="btn" OnClick="Button2_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>