﻿using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Core.Tools;

namespace Krosoft.Extensions.Core.Extensions;

/// <summary>
/// Méthodes d'extensions pour la classe <see cref="Stream" />.
/// </summary>
public static class StreamExtension
{
    public static byte[] ReadAsByteArray(this Stream stream)
    {
        Guard.IsNotNull(nameof(stream), stream);
        return StreamHelper.ReadAsByteArray(stream);
    }

    /// <summary>
    /// Ecrit un stream dans un fichier.
    /// </summary>
    /// <param name="filePath">Chemin du fichier.</param>
    /// <param name="stream">Contenu du fichier.</param>
    /// <returns>Tache asynchrone.</returns>
    public static async Task WriteAsync(this Stream stream, string filePath)
    {
        Guard.IsNotNullOrWhiteSpace(nameof(filePath), filePath);
        Guard.IsNotNull(nameof(stream), stream);

        await FileHelper.WriteAsync(filePath, stream).ConfigureAwait(false);
    }

    /// <summary>
    /// Ecrit un stream dans un fichier.
    /// </summary>
    /// <param name="filePath">Chemin du fichier.</param>
    /// <param name="stream">Contenu du fichier.</param>
    public static void Write(this Stream stream, string filePath)
    {
        Guard.IsNotNullOrWhiteSpace(nameof(filePath), filePath);
        Guard.IsNotNull(nameof(stream), stream);

        FileHelper.Write(filePath, stream);
    }

    public static T To<T>(this Stream stream)
    {
        Guard.IsNotNull(nameof(stream), stream);

        if (typeof(T) == typeof(Stream))
        {
            return (T)(object)stream;
        }

        if (typeof(T) == typeof(byte[]))
        {
            using (var reader = new BinaryReader(stream))
            {
                return (T)(object)reader.ReadAllBytes();
            }
        }

        if (typeof(T) == typeof(string))
        {
            using (var reader = new StreamReader(stream))
            {
                // Read the content.
                var content = reader.ReadToEnd();
                // Display the content.
                return (T)(object)content;
            }
        }

        throw new KrosoftTechniqueException($"Le type {typeof(T)} n'est pas géré.");
    }
}