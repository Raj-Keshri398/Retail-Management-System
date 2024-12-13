<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style type="text/css">
        body {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
        }

        .container {
            align-items: center;
            justify-content: center;
            height: 297px;
            width: 436px;
            border: 1px solid #000; /* optional border for visibility */
        }

        .title {
            margin: 0;
            background-color: #000;
            width: 100%;
            box-sizing: border-box;
            color: #fff;
            padding: 5px;
            /* padding: top right bottom left */
            font-size: 24px;
            text-align: center;
        }

        .box {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            background-color: #fff;
            width: 100%;
            height: 297px;
            box-sizing: border-box;
            border: 1px solid #000;
            margin-right: 30px;
            /*box-shadow: x y blur size color;*/
            box-shadow: 0px 0px 5px #000;
            border-radius: 4px;
        }

        .fields {
            display: flex;
            padding: 10px 20px;
            /* padding 10px means top and bottom padding 20px means left and right */
            margin-top: 30px;
            margin-bottom: 20px;
        }

        .label {
            margin-left: 20px;
            font-size: 18px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        }

        .buttonContainer {
            padding: 10px 20px;
        }

        .ml-10 {
            margin-left: 50px;
            margin-right: 30px;
        }

        .btn {
            padding: 8px 24px;
            background-color: #6fa1a1;
            border: 2px solid #6fa1a1;
            border-radius: 8px;
            cursor: pointer;
        }
    </style>

    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h3 class="title">Login Here</h3>
            <div class="box">
                <div class="fields">
                    <asp:Label ID="Label1" runat="server" Text="User ID" CssClass="label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="ml-10" Height="18px" Width="168px"></asp:TextBox>
                </div>
                <div class="fields">
                    <asp:Label ID="Label2" runat="server" Text="Password" CssClass="label"></asp:Label>
&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="ml-10" Height="18px" Width="168px" TextMode="Password"></asp:TextBox>
                </div>
                <div class="buttonContainer">
                    <asp:Button ID="Button1" runat="server" Text="Clear it" CssClass="btn ml-10" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Login" CssClass="btn ml-10" OnClick="Button2_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
