﻿using System.Collections.Generic;

namespace Krosoft.Extensions.Samples.Library.Models;

public class UtilisateurEqualityComparer : IEqualityComparer<Utilisateur>
{
    public bool Equals(Utilisateur? x, Utilisateur? y) => y != null && x != null && x.Nom == y.Nom && x.Prenom == y.Prenom;

    /// <summary>
    /// Retourne un code de hachage pour l'objet spécifié.
    /// </summary>
    /// <returns>
    /// Code de hachage pour l'objet spécifié.
    /// </returns>
    /// <param name="obj"><see cref="T:System.Object" /> pour lequel un code de hachage doit être retourné.</param>
    public int GetHashCode(Utilisateur obj) => base.GetHashCode();
}