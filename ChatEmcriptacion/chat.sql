-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3306
-- Tiempo de generación: 16-06-2020 a las 22:31:52
-- Versión del servidor: 5.6.48-log
-- Versión de PHP: 7.3.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `chat`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mensajeria`
--

DROP TABLE IF EXISTS `mensajeria`;
CREATE TABLE IF NOT EXISTS `mensajeria` (
  `idmen` int(11) NOT NULL AUTO_INCREMENT,
  `idemisor` int(11) NOT NULL,
  `mensaje` text NOT NULL,
  `fecha` datetime NOT NULL,
  PRIMARY KEY (`idmen`),
  KEY `idemisor` (`idemisor`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `idusu` int(11) NOT NULL AUTO_INCREMENT,
  `nombres` text NOT NULL,
  `apellidos` text NOT NULL,
  `correo` text NOT NULL,
  `contrasena` text NOT NULL,
  PRIMARY KEY (`idusu`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `mensajeria`
--
ALTER TABLE `mensajeria`
  ADD CONSTRAINT `mensajeria_ibfk_1` FOREIGN KEY (`idemisor`) REFERENCES `usuario` (`idusu`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
