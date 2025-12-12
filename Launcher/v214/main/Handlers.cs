using System;
using System.Windows.Forms;

namespace Moonlight
{
    internal class Handlers
    {
        public static string getAuthTokenFromInput(InPacket inPacket)
        {

            try
            {
                MessageBox.Show($"[DEBUG] getAuthTokenFromInput: packet length = {inPacket.len}");

                // 打印整个数据包的十六进制内容
                MessageBox.Show($"[DEBUG] Full packet data (hex):");
                for (int i = 0; i < Math.Min(inPacket.len, 100); i++) // 只打印前100字节
                {
                    MessageBox.Show($"{inPacket.buf[i]:X2} ");
                    if ((i + 1) % 16 == 0) Console.WriteLine();
                }

                // 读取并跳过包头（假设前4字节是长度）
                int packetLength = inPacket.readInt();
                MessageBox.Show($"[DEBUG] Packet length from header: {packetLength}");

                // 读取操作码
                short opcode = inPacket.readShort();
                MessageBox.Show($"[DEBUG] Opcode: {opcode}");

                // 读取认证结果
                byte authResult = inPacket.readByte();
                MessageBox.Show($"[DEBUG] Auth result: {authResult}");

                if (authResult == 0) // 假设0表示成功
                {
                    string token = inPacket.readString(); // 或者根据您的协议读取token
                    MessageBox.Show($"[DEBUG] Token extracted: {token}");
                    return token;
                }
                else
                {
                    MessageBox.Show($"[DEBUG] Authentication failed with code: {authResult}");
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[DEBUG] Exception in getAuthTokenFromInput: {ex}");
                return "";
            }
        }
    }
}
