using System.Security.Cryptography;

namespace Sim.Application.Helpers;

public static class SecureRandom
{
    public static int GenerateSecureRandomNumber()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] randomNumber = new byte[4]; // 4 bytes = 32 bits = int
        rng.GetBytes(randomNumber);
        return BitConverter.ToInt32(randomNumber, 0) & int.MaxValue; // Para garantir que seja positivo
    }

}

