//namespace Shop.Contract;

//public static class GuidV7
//{
//    public static Guid NewGuid()
//    {
//        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
//        var randomBytes = new byte[8];

//        var random = new Random();
//        random.NextBytes(randomBytes);

//        // Thay đổi các byte để tạo UUID v7
//        var guidBytes = new byte[16];
//        var timestampBytes = BitConverter.GetBytes(timestamp);
//        Array.Copy(timestampBytes, 0, guidBytes, 0, 8);
//        Array.Copy(randomBytes, 0, guidBytes, 8, 8);

//        // Sửa bit để đảm bảo UUID là UUID v7
//        guidBytes[6] &= (byte)0x0f; // Set the version (7) in the 7th byte
//        guidBytes[6] |= (byte)0x70;

//        return new Guid(guidBytes);
//    }
//}
