# Liste des fonctions du projet jdPoint

## Fichier : `Form1.cs`

### Constructeurs et initialisation
- **`Form1()`** (ligne 17) - Constructeur principal du formulaire
- **`Form1_Shown(object? sender, EventArgs e)`** (ligne 34) - Gestionnaire d'événement lors de l'affichage du formulaire

### Gestion de la grille
- **`btnApply_Click(object sender, EventArgs e)`** (ligne 46) - Applique les dimensions de la grille
- **`GridValueChanged(object? sender, EventArgs e)`** (ligne 51) - Gère les changements de valeurs de la grille
- **`GridPanelResize(object? sender, EventArgs e)`** (ligne 61) - Gère le redimensionnement du panneau de grille
- **`UpdateGridDimensions()`** (ligne 66) - Met à jour les dimensions de la grille
- **`RestartGame()`** (ligne 228) - Redémarre le jeu

### Sauvegarde et chargement
- **`btnSave_Click(object sender, EventArgs e)`** (ligne 78) - Sauvegarde l'état du jeu dans la base de données
- **`btnLoad_Click(object sender, EventArgs e)`** (ligne 132) - Charge un état de jeu depuis la base de données
- **`GetConnectionString()`** (ligne 539) - Récupère la chaîne de connexion PostgreSQL
- **`EnsureDatabaseObjects()`** (ligne 544) - Crée/maintient les objets de la base de données

### Gestion des actions du jeu
- **`ActionSelectionChanged(object? sender, EventArgs e)`** (ligne 239) - Gère le changement de sélection d'action
- **`MissileParametersChanged(object? sender, EventArgs e)`** (ligne 245) - Gère les changements de paramètres de missile
- **`UpdateCurrentPlayerDisplay()`** (ligne 250) - Met à jour l'affichage du joueur actuel
- **`UpdateMissileControls()`** (ligne 258) - Met à jour les contrôles de missiles

### Calcul des missiles
- **`CalculateTargetRow(int power, int gridSize)`** (ligne 275) - Calcule la ligne cible du missile
- **`CalculateTargetColumn(int power, int gridSize)`** (ligne 282) - Calcule la colonne cible du missile
- **`btnFireMissile_Click(object? sender, EventArgs e)`** (ligne 289) - Gère le tir de missile
- **`SwitchPlayer()`** (ligne 331) - Change de joueur

### Gestion des points et de la grille
- **`pnlGrid_MouseClick(object sender, MouseEventArgs e)`** (ligne 338) - Gère les clics sur la grille
- **`pnlGrid_Paint(object sender, PaintEventArgs e)`** (ligne 485) - Dessine la grille et les points

### Vérification des alignements
- **`HasFiveInRow(Point origin, bool playerColor)`** (ligne 399) - Vérifie s'il y a 5 points alignés
- **`MarkAlignedPointsAsInvulnerable(Point origin, bool playerColor)`** (ligne 407) - Marque les points alignés comme invulnérables
- **`MarkDirectionAsInvulnerable(Point origin, int dx, int dy, bool playerColor)`** (ligne 419) - Marque les points dans une direction comme invulnérables
- **`CountAligned(Point origin, int dx, int dy, bool playerColor)`** (ligne 455) - Compte les points alignés
- **`CountDirection(Point origin, int dx, int dy, bool playerColor)`** (ligne 463) - Compte les points dans une direction

### Types et enregistrements
- **`PointState` (record)** (ligne 578) - Enregistrement pour l'état des points

---

## Fichier : `Program.cs`

### Point d'entrée
- **`Main()`** (ligne 9) - Point d'entrée principal de l'application

---

## Fichier : `Form1.Designer.cs`

### Méthodes générées automatiquement
- **`InitializeComponent()`** - Initialise tous les composants du formulaire
- **Dispose()** - Libère les ressources utilisées par le formulaire

---

## Résumé des catégories de fonctions

### 🎮 Logique du jeu (8 fonctions)
- Gestion des tours, placement des points, vérification des victoires

### 🚀 Système de missiles (4 fonctions)  
- Calcul des cibles, tir de missiles, gestion des impacts

### 💾 Base de données (3 fonctions)
- Sauvegarde, chargement, connexion PostgreSQL

### 🎨 Interface graphique (4 fonctions)
- Dessin de la grille, gestion des clics, mise à jour de l'affichage

### 🔧 Utilitaires (5 fonctions)
- Initialisation, redimensionnement, changement de joueur

### 📊 Alignements (5 fonctions)
- Détection des alignements de 5 points, marquage des points invulnérables

**Total : 29 fonctions principales + fonctions générées automatiques**
