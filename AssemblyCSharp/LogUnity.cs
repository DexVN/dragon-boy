
using UnityEngine;

namespace AssemblyCSharp
{
    public class LogUnity
    {
        protected static LogUnity instance;
        public static LogUnity gI()
        {
            if (instance == null)
                instance = new LogUnity();
            return instance;
        }

        public void LogMessageContent(Message msg)
        {
            if (msg == null || msg.reader() == null) return;

            myReader reader = msg.reader();
            int avail = reader.available();

            // Nếu dữ liệu quá ngắn, không phải format readUTF (cần ít nhất 2 byte length)
            if (avail < 2) return;

            try
            {
                // Đánh dấu vị trí hiện tại
                reader.mark(avail + 10);

                // Thử đọc chuỗi UTF từ reader của bạn
                string content = reader.readUTF();

                if (!string.IsNullOrEmpty(content))
                {
                    // Kiểm tra sơ bộ xem chuỗi có "sạch" không (tránh log nhị phân nhầm)
                    // Nếu chuỗi chứa quá nhiều ký tự điều khiển (ASCII < 32), ta bỏ qua
                    int controlCount = 0;
                    for (int i = 0; i < Math.min(content.Length, 10); i++)
                    {
                        if (char.IsControl(content[i]) && !char.IsWhiteSpace(content[i]))
                            controlCount++;
                    }

                    if (controlCount < 2)
                    {
                        Debug.Log($"[MSG LOG] CMD: {msg.command} | Content: {content}");
                    }
                }
            }
            catch
            {
                // Nếu không phải chuỗi UTF-8, catch sẽ chặn lỗi để không crash game
            }
            finally
            {
                // Luôn luôn reset để Message không bị mất dữ liệu khi game xử lý thật
                try { reader.reset(); } catch { }
            }
        }
    }
}
