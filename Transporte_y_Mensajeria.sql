CREATE DATABASE  IF NOT EXISTS `transporte_y_mensajeria` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `transporte_y_mensajeria`;
-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: localhost    Database: transporte_y_mensajeria
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `aviones`
--

DROP TABLE IF EXISTS `aviones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aviones` (
  `idAvion` int(11) NOT NULL AUTO_INCREMENT,
  `modelo` varchar(100) DEFAULT NULL,
  `aumento` double NOT NULL,
  `fechaCompra` datetime DEFAULT NULL,
  `precioCompra` double DEFAULT NULL,
  `fk_idSupervisorA` int(11) DEFAULT NULL,
  PRIMARY KEY (`idAvion`),
  KEY `fk_idSupervisorA_idx` (`fk_idSupervisorA`),
  CONSTRAINT `fk_idSupervisorA` FOREIGN KEY (`fk_idSupervisorA`) REFERENCES `supervisores` (`idSupervisor`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aviones`
--

LOCK TABLES `aviones` WRITE;
/*!40000 ALTER TABLE `aviones` DISABLE KEYS */;
INSERT INTO `aviones` VALUES (3,'Boeing 747',10,'1990-05-25 00:00:00',404000000,NULL),(4,' Boeing 777-200',10,'1989-11-30 00:00:00',560000000,NULL);
/*!40000 ALTER TABLE `aviones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `idCliente` int(11) NOT NULL AUTO_INCREMENT,
  `cuil` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `direccion` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  PRIMARY KEY (`idCliente`),
  UNIQUE KEY `cuil_UNIQUE` (`cuil`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (8,111112,'Juan','Perez','Calle 111','0381-411111'),(9,111113,'Juan','Perez','Calle 111','0381-411111'),(27,39128748,'Roberto','Gomez','Mendoza 345','03814879854'),(28,23456478,'Ricardo','Juanes','Suipacha','0381-411111'),(29,345234,'Juan','Perez','Calle 111','0381-411111'),(30,11111,'Juan','Perez','Calle 111','0381-411111');
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `envios`
--

DROP TABLE IF EXISTS `envios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `envios` (
  `idEnvios` int(11) NOT NULL AUTO_INCREMENT,
  `fechaEnvio` datetime NOT NULL,
  `fk_idClienteEmisor` int(11) NOT NULL,
  `fk_idClienteReceptor` int(11) NOT NULL,
  `fk_idPaquete` int(11) DEFAULT NULL,
  `fk_idSobre` int(11) DEFAULT NULL,
  `precioFinal` double DEFAULT NULL,
  PRIMARY KEY (`idEnvios`),
  KEY `fk_idClienteEmisor_idx` (`fk_idClienteEmisor`),
  KEY `fk_idClienteReceptor_idx` (`fk_idClienteReceptor`),
  KEY `fk_idPaquete_idx` (`fk_idPaquete`),
  KEY `fk_idSobre_idx` (`fk_idSobre`),
  CONSTRAINT `fk_idClienteEmisor` FOREIGN KEY (`fk_idClienteEmisor`) REFERENCES `clientes` (`idCliente`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idClienteReceptor` FOREIGN KEY (`fk_idClienteReceptor`) REFERENCES `clientes` (`idCliente`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idPaquete` FOREIGN KEY (`fk_idPaquete`) REFERENCES `paquetes` (`idPaquete`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idSobre` FOREIGN KEY (`fk_idSobre`) REFERENCES `sobres` (`idSobre`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `envios`
--

LOCK TABLES `envios` WRITE;
/*!40000 ALTER TABLE `envios` DISABLE KEYS */;
INSERT INTO `envios` VALUES (1,'2019-01-01 00:00:00',8,9,3,NULL,1000),(2,'2019-01-02 00:00:00',8,9,NULL,4,500),(3,'2019-02-01 00:00:00',8,9,3,NULL,2000),(4,'2019-02-02 00:00:00',8,9,NULL,4,1000),(5,'2019-03-01 00:00:00',8,9,3,NULL,1000),(6,'2019-03-02 00:00:00',8,9,NULL,4,500),(7,'2019-04-01 00:00:00',8,9,3,NULL,3000),(8,'2019-04-02 00:00:00',8,9,NULL,4,1500),(9,'2019-05-01 00:00:00',8,9,3,NULL,6000),(10,'2019-05-02 00:00:00',8,9,NULL,4,1500),(11,'2019-06-01 00:00:00',8,9,3,NULL,5000),(12,'2019-06-02 00:00:00',8,9,NULL,4,5000),(13,'2019-07-01 00:00:00',8,9,3,NULL,500),(14,'2019-07-02 00:00:00',8,9,NULL,4,5000),(15,'2019-08-01 00:00:00',8,9,3,NULL,3000),(16,'2019-08-02 00:00:00',8,9,NULL,4,4000),(17,'2019-09-01 00:00:00',8,9,3,NULL,3500),(18,'2019-09-02 00:00:00',8,9,NULL,4,3500),(19,'2019-10-01 00:00:00',8,9,3,NULL,3500),(20,'2019-10-02 00:00:00',8,9,NULL,4,3000),(21,'2019-11-01 00:00:00',8,9,3,NULL,5000),(22,'2019-11-02 00:00:00',8,9,NULL,4,5000),(23,'2019-12-01 00:00:00',8,9,3,NULL,500),(24,'2019-12-02 00:00:00',8,9,NULL,4,8000),(25,'2018-01-01 00:00:00',8,9,3,NULL,500),(26,'2019-12-03 00:00:00',30,27,5,NULL,907.5000000000002),(27,'2019-06-17 00:00:00',30,27,7,NULL,1773.7500000000005),(28,'2019-12-03 00:00:00',30,27,8,NULL,709.5),(29,'2019-12-03 00:00:00',30,27,NULL,8,23100),(30,'2019-12-03 00:00:00',30,27,7,NULL,1773.7500000000005),(31,'2019-12-03 00:00:00',30,27,8,NULL,709.5),(32,'2019-12-03 00:00:00',30,27,7,NULL,1773.7500000000005),(33,'2019-12-03 00:00:00',30,27,NULL,8,23100),(34,'2019-12-03 00:00:00',30,27,NULL,8,23100);
/*!40000 ALTER TABLE `envios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `furgonetas`
--

DROP TABLE IF EXISTS `furgonetas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `furgonetas` (
  `idFurgoneta` int(11) NOT NULL AUTO_INCREMENT,
  `modelo` varchar(100) DEFAULT NULL,
  `aumento` double NOT NULL,
  `fechaCompra` datetime DEFAULT NULL,
  `precioCompra` double DEFAULT NULL,
  `capacidadCarga` double DEFAULT NULL,
  `fk_idSupervisorF` int(11) DEFAULT NULL,
  PRIMARY KEY (`idFurgoneta`),
  KEY `fk_idSupervisorF_idx` (`fk_idSupervisorF`),
  CONSTRAINT `fk_idSupervisorF` FOREIGN KEY (`fk_idSupervisorF`) REFERENCES `supervisores` (`idSupervisor`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `furgonetas`
--

LOCK TABLES `furgonetas` WRITE;
/*!40000 ALTER TABLE `furgonetas` DISABLE KEYS */;
INSERT INTO `furgonetas` VALUES (3,'For Fiesta Van',5,'2010-09-12 00:00:00',180000,50,5);
/*!40000 ALTER TABLE `furgonetas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `motos`
--

DROP TABLE IF EXISTS `motos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `motos` (
  `idMoto` int(11) NOT NULL AUTO_INCREMENT,
  `modelo` varchar(100) DEFAULT NULL,
  `aumento` double NOT NULL,
  `fechaCompra` datetime DEFAULT NULL,
  `precioCompra` double DEFAULT NULL,
  `cilindrada` int(11) DEFAULT NULL,
  `fk_idSupervisorM` int(11) DEFAULT NULL,
  PRIMARY KEY (`idMoto`),
  KEY `fk_idSupervisorM_idx` (`fk_idSupervisorM`),
  CONSTRAINT `fk_idSupervisorM` FOREIGN KEY (`fk_idSupervisorM`) REFERENCES `supervisores` (`idSupervisor`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `motos`
--

LOCK TABLES `motos` WRITE;
/*!40000 ALTER TABLE `motos` DISABLE KEYS */;
INSERT INTO `motos` VALUES (2,'test2',2,'2019-12-07 00:00:00',2500,75,NULL),(6,'C90',2,'2019-03-12 00:00:00',2500,75,5),(7,'Yamaha',2,'2018-07-16 00:00:00',68050,250,NULL);
/*!40000 ALTER TABLE `motos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paquetes`
--

DROP TABLE IF EXISTS `paquetes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `paquetes` (
  `idPaquete` int(11) NOT NULL AUTO_INCREMENT,
  `precioNeto` double NOT NULL,
  `contenido` varchar(100) NOT NULL,
  `asegurada` tinyint(4) NOT NULL,
  `aumSeguro` double NOT NULL,
  `largoRecorrido` tinyint(4) NOT NULL,
  `volumen` double NOT NULL,
  `fk_idPrecioUnidadP` int(11) DEFAULT NULL,
  `fk_idFurgoneta` int(11) DEFAULT NULL,
  `fk_idAvionP` int(11) DEFAULT NULL,
  PRIMARY KEY (`idPaquete`),
  KEY `fk_idPrecioUnidadP_idx` (`fk_idPrecioUnidadP`),
  KEY `fk_idFurgoneta_idx` (`fk_idFurgoneta`),
  KEY `fk_idAvionP_idx` (`fk_idAvionP`),
  CONSTRAINT `fk_idAvionP` FOREIGN KEY (`fk_idAvionP`) REFERENCES `aviones` (`idAvion`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idFurgoneta` FOREIGN KEY (`fk_idFurgoneta`) REFERENCES `furgonetas` (`idFurgoneta`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idPrecioUnidadP` FOREIGN KEY (`fk_idPrecioUnidadP`) REFERENCES `precio_unidad` (`idPrecioUnidad`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paquetes`
--

LOCK TABLES `paquetes` WRITE;
/*!40000 ALTER TABLE `paquetes` DISABLE KEYS */;
INSERT INTO `paquetes` VALUES (3,110.00000000000001,'Proyector',1,10,0,1,NULL,NULL,NULL),(5,5.5,'Zapatillas',1,10,0,5,NULL,NULL,3),(6,200,'Televisor',0,10,1,2,NULL,NULL,NULL),(7,550,'Radio',1,10,1,5,NULL,3,3),(8,220.00000000000003,'tele',1,10,1,2,NULL,3,3),(9,550,'Parlante',1,10,1,5,NULL,3,3);
/*!40000 ALTER TABLE `paquetes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `precio_unidad`
--

DROP TABLE IF EXISTS `precio_unidad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `precio_unidad` (
  `idPrecioUnidad` int(11) NOT NULL AUTO_INCREMENT,
  `unidad` varchar(50) NOT NULL,
  `fecha` datetime NOT NULL,
  `precio` double NOT NULL,
  PRIMARY KEY (`idPrecioUnidad`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `precio_unidad`
--

LOCK TABLES `precio_unidad` WRITE;
/*!40000 ALTER TABLE `precio_unidad` DISABLE KEYS */;
/*!40000 ALTER TABLE `precio_unidad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sobres`
--

DROP TABLE IF EXISTS `sobres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sobres` (
  `idSobre` int(11) NOT NULL AUTO_INCREMENT,
  `precioNeto` double NOT NULL,
  `contenido` varchar(100) NOT NULL,
  `asegurada` tinyint(4) NOT NULL,
  `aumSeguro` double NOT NULL,
  `largoRecorrido` tinyint(4) NOT NULL,
  `peso` double NOT NULL,
  `fk_idPrecioUnidadS` int(11) DEFAULT NULL,
  `fk_idMoto` int(11) DEFAULT NULL,
  `fk_idAvionS` int(11) DEFAULT NULL,
  PRIMARY KEY (`idSobre`),
  KEY `fk_idPrecioUnidad_idx` (`fk_idPrecioUnidadS`),
  KEY `fk_idMoto_idx` (`fk_idMoto`),
  KEY `fk_idAvionS_idx` (`fk_idAvionS`),
  CONSTRAINT `fk_idAvionS` FOREIGN KEY (`fk_idAvionS`) REFERENCES `aviones` (`idAvion`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idMoto` FOREIGN KEY (`fk_idMoto`) REFERENCES `motos` (`idMoto`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_idPrecioUnidadS` FOREIGN KEY (`fk_idPrecioUnidadS`) REFERENCES `precio_unidad` (`idPrecioUnidad`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sobres`
--

LOCK TABLES `sobres` WRITE;
/*!40000 ALTER TABLE `sobres` DISABLE KEYS */;
INSERT INTO `sobres` VALUES (4,165,'Propaganda',1,10,1,150,NULL,NULL,NULL),(7,550,'Teclado',1,10,1,500,NULL,6,NULL),(8,110.00000000000001,'Zapatillas',1,10,1,100,NULL,6,3);
/*!40000 ALTER TABLE `sobres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supervisores`
--

DROP TABLE IF EXISTS `supervisores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supervisores` (
  `idSupervisor` int(11) NOT NULL AUTO_INCREMENT,
  `cuil` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `direccion` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  PRIMARY KEY (`idSupervisor`),
  UNIQUE KEY `cuil_UNIQUE` (`cuil`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supervisores`
--

LOCK TABLES `supervisores` WRITE;
/*!40000 ALTER TABLE `supervisores` DISABLE KEYS */;
INSERT INTO `supervisores` VALUES (5,222222,'Maria','Gomez','Calle 222','0381-422222');
/*!40000 ALTER TABLE `supervisores` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-04 13:55:48
