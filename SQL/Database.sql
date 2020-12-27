SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';
set SQL_SAFE_UPDATES = 0;

-- -----------------------------------------------------
-- Schema Projet_Cooking
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Projet_Cooking` DEFAULT CHARACTER SET utf8 ;
USE `Projet_Cooking` ;

-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Client`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Client` (
  `idClient` INT NOT NULL AUTO_INCREMENT,
  `Nom_Client` VARCHAR(50) NULL,
  `Telephone_Client` VARCHAR(12) NULL,
  `Adresse_Client` VARCHAR(256) NULL,
  `Portefeuille_Cook` INT NULL,
  `Identifiant_Cooking` VARCHAR(30) NULL,
  `MotdePasse_Cooking` VARCHAR(45) NULL,
  PRIMARY KEY (`idClient`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Createur de Recettes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Createur de Recettes` (
  `idCreateur de Recettes` INT NOT NULL AUTO_INCREMENT,
  `Nombre_Recettes_Crees` INT NULL,
  `Client_idClient` INT NOT NULL,
  PRIMARY KEY (`idCreateur de Recettes`, `Client_idClient`),
  CONSTRAINT `fk_Createur de Recettes_Client1`
    FOREIGN KEY (`Client_idClient`)
    REFERENCES `Projet_Cooking`.`Client` (`idClient`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Commande`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Commande` (
  `Reference_Commande` INT NOT NULL AUTO_INCREMENT,
  `Date_Commande` DATE NULL,
  `Client_idClient` INT NOT NULL,
  PRIMARY KEY (`Reference_Commande`),
  CONSTRAINT `fk_Commande_Client1`
    FOREIGN KEY (`Client_idClient`)
    REFERENCES `Projet_Cooking`.`Client` (`idClient`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Recette`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Recette` (
  `idRecette` INT NOT NULL AUTO_INCREMENT,
  `Nom_Recette` VARCHAR(50) NULL,
  `Type_Recette` VARCHAR(50) NULL,
  `Descriptif_Recette` VARCHAR(256) NULL,
  `Prix_Recette` INT NULL,
  `QuantitéVendue_Recette` INT NULL,
  `Remuneration_CdR` INT NULL,
  `Createur de Recettes_idCreateur de Recettes` INT NOT NULL,
  `Createur de Recettes_Client_idClient` INT NOT NULL,
  PRIMARY KEY (`idRecette`),
  CONSTRAINT `fk_Recette_Createur de Recettes1`
    FOREIGN KEY (`Createur de Recettes_idCreateur de Recettes`)
    REFERENCES `Projet_Cooking`.`Createur de Recettes` (`idCreateur de Recettes`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Details_Commande`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Details_Commande` (
  `Commande_Reference_Commande` INT NOT NULL,
  `Recette_idRecette` INT NOT NULL,
  `Quantité_Recette_Commande` INT NULL,
  PRIMARY KEY (`Commande_Reference_Commande`, `Recette_idRecette`),
  CONSTRAINT `fk_Commande_has_Recette_Commande1`
    FOREIGN KEY (`Commande_Reference_Commande`)
    REFERENCES `Projet_Cooking`.`Commande` (`Reference_Commande`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Commande_has_Recette_Recette1`
    FOREIGN KEY (`Recette_idRecette`)
    REFERENCES `Projet_Cooking`.`Recette` (`idRecette`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Fournisseur`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Fournisseur` (
  `idFournisseur` INT NOT NULL,
  `Nom_Fournisseur` VARCHAR(45) NULL,
  `Telephone_Fournisseur` VARCHAR(10) NULL,
  PRIMARY KEY (`idFournisseur`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Produit`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Produit` (
  `idProduit` INT NOT NULL AUTO_INCREMENT,
  `Nom_Produit` VARCHAR(100) NULL,
  `Catégorie_Produit` VARCHAR(100) NULL,
  `Unite_Produit` VARCHAR(20) NULL,
  `Stock_Produit` INT NULL,
  `StockMin_Produit` INT NULL,
  `StockMax_Produit` INT NULL,
  `LastUse` DATE NULL,
  `Fournisseur_idFournisseur` INT NOT NULL,
  PRIMARY KEY (`idProduit`),
  CONSTRAINT `fk_Produit_Fournisseur1`
    FOREIGN KEY (`Fournisseur_idFournisseur`)
    REFERENCES `Projet_Cooking`.`Fournisseur` (`idFournisseur`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Projet_Cooking`.`Ingrédients_Recette`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Projet_Cooking`.`Ingrédients_Recette` (
  `Recette_idRecette` INT NOT NULL,
  `Produit_idProduit` INT NOT NULL,
  `Quantité_Produit` INT NULL,
  PRIMARY KEY (`Recette_idRecette`, `Produit_idProduit`),
  CONSTRAINT `fk_Recette_has_Produit_Recette1`
    FOREIGN KEY (`Recette_idRecette`)
    REFERENCES `Projet_Cooking`.`Recette` (`idRecette`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Recette_has_Produit_Produit1`
    FOREIGN KEY (`Produit_idProduit`)
    REFERENCES `Projet_Cooking`.`Produit` (`idProduit`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- insertion dans la table client 
INSERT INTO `projet_cooking`.`client` (`Nom_Client`,`Telephone_Client`,`Adresse_Client`,`Portefeuille_Cook`,`Identifiant_Cooking`,`MotdePasse_Cooking`) VALUES ('Cooking','0102030405','26 rue de la Paix, 75001 Paris',2000,'cooking','admin');
INSERT INTO `projet_cooking`.`client` (`Nom_Client`,`Telephone_Client`,`Adresse_Client`,`Portefeuille_Cook`,`Identifiant_Cooking`,`MotdePasse_Cooking`) VALUES ('Macquart','0690501020','27 rue du SkatePark, 77700 Magny',300,'hmacquart','petitcoeur');
-- On créé l'admin et le premier client

-- insertion dans la table createur de recettes
INSERT INTO `projet_cooking`.`createur de recettes`(`Nombre_Recettes_Crees`,`Client_idClient`) VALUES (6,001);
-- Le client 1 devient le CdR 001 ayant créé 5 recettes
INSERT INTO `projet_cooking`.`createur de recettes`(`Nombre_Recettes_Crees`,`Client_idClient`) VALUES (2,002);

-- insertion dans la table recette
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Pates au Saumon", "Plat", "Tagliatelles de saumon et crème fraîche",30,0,2,001,001);
-- La recette 001 est Pates au saumon, qui est un plat avec sa description, vendu 30 cooks, vendue 0 fois pour le moment et qui est une recette créée par le CdR 001 qui est le client 001
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Poulet au curry", "Plat", "Poulet accompagné de son riz gluant et de sa sauce au curry ",40,0,2,002,002);
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Tiramisu", "Dessert", "Tiramisu",20,0,2,001,001);
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Linguine aux crevettes", "Plat", "Linguine à la crème et crevettes",35,0,2,002,002);
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Oeufs benedictes", "Entrée", "Oeufs sur leurs tranches de pain de campagne ",30,0,2,001,001);
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Fondant au chocolat", "Dessert", "Fondant au chocolat noir de Tanzanie",25,0,2,001,001);
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Iles flottantes", "Dessert", "Iles Flottantes aux arômes de vanille",20,0,2,001,001);
INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ("Crumble aux fruits rouges", "Dessert", "Crumble aux 3 fruits champêtres",15,0,2,001,001);

-- insertion dans la table fournisseur
INSERT INTO `projet_cooking`.`fournisseur`(`idFournisseur`,`Nom_Fournisseur`,`Telephone_Fournisseur`) VALUES (001,'Carrefour','0148752256');
INSERT INTO `projet_cooking`.`fournisseur`(`idFournisseur`,`Nom_Fournisseur`,`Telephone_Fournisseur`) VALUES (002,'MiamLand','0156887523');

-- insertion dans la table produit
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Crème Fraîche','Produit Laitier','cL',200,100,300,NOW(), 001);
-- Le produit 001 est la Crème Fraiche, qui est un complément, comptabilisé en centilitres (cL), avec un stock minimal de 100, maximal de 300 et actuel de 200 fourni par Carrefour
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Saumon','Poisson','g',15,10,30,NOW(), 001);
-- Le produit 002 est le Saumon, qui est un poisson, comptabilisé en tranches, avec un stock minimal de 10, maximal de 30 et actuel de 15 fourni par Carrefour
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Pates','Accompagnement','g',5000,3000,6000,NOW(), 001);
-- Le produit 003 est les Pates, qui est un accompagnement, comptabilisé en grammes (g), avec un stock minimal de 3000, maximal de 6000 et actuel de 5000 fourni par Carrefour
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Riz Basmati','Accompagnement','g',8000,3000,10000,NOW(),001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Poulet','Viande','g',2000,1000,4000,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Curry Indien','Autre','g',1000,500,2000,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Oeufs','Autre','Unité',100,70,200,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Mascarpone','Produit Laitier','g',4000,2000,6000,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Sucre en poudre','Autre','g',2000,1000,5000,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`)VALUES ('Café','Boisson','cL',500,200,800, NOW(),002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`)VALUES ('Poudre de Cacao','Autre','g',100,50,200, NOW(),001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Linguine','Autre','g',8000,3000,10000,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Crevettes','Poisson','Unité',150,80,300,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Ail','Autre','g',50,20,100, NOW(),001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Tranche de Pain','Autre','Unité',140,70,200,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Mayonnaise','Autre','cL',200,100,300, NOW(),002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Chocolat noir en tablettes','Autre','g',2000,1000,3000, NOW(),002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`)VALUES ('Beurre','Produit Laitier','g',2000,1500,3000, NOW(),002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Farine','Autre','g',1000,500,2000,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Lait','Produit Laitier','cL',1000,500,2000, NOW(),001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`)VALUES ('Vanille','Autre','g',30,20,50,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`)VALUES ('Amandes','Fruit','g',600,300,1500, NOW(),002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`)VALUES ('Sucre en morceaux','Autre','g',2000,1000,5000, NOW(),001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Jus de Citron','Autre','cL',150,100,200,NOW(), 001);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Sel','Autre','g',500,200,1000,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Fraises','Fruit','g',800,300,1000,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Groseilles','Fruit','g',800,300,1000,NOW(), 002);
INSERT INTO `projet_cooking`.`produit`(`Nom_Produit`,`Catégorie_Produit`,`Unite_Produit`,`Stock_Produit`,`StockMin_Produit`,`StockMax_Produit`,`LastUse`,`Fournisseur_iDFournisseur`) VALUES ('Framboises','Fruit','g',800,300,1000,NOW(), 002);

-- insertion dans la table ingredients_recette
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (001,001,10);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (001,002,2);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (001,003,150);
-- On récupère les ingrédients de la recette 1 (Pates Saumon) qu'on prépare avec 10 produit 001(cL de Creme Fraiche), 2 produits 002 (tranches de saumon), 150 produits 003 (grammes de pates)
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (002,004,100);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (002,005,1);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (002,006,2);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (002,001,10);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (003,007,3);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (003,008,25);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (003,009,100);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (003,010,20);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (003,011,3);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (004,012,150);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (004,001,10);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (004,013,6);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (004,014,1);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (005,07,2);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (005,015,2);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (005,016,15);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (006,017,200);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (006,018,150);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (006,009,150);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (006,019,50);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (006,007,3);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,007,4);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,020,60);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,021,1);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,022,30);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,009,110);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,023,60);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,024,5);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (007,025,1);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (008,009,150);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (008,018,125);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (008,026,250);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (008,027,250);
INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (008,028,250);

/*
drop table if exists ingrédients_recette;
drop table if exists produit;
drop table if exists fournisseur;
drop table if exists details_commande;
drop table if exists recette;
drop table if exists commande;
drop table if exists `createur de recettes`;
drop table if exists client;
*/
