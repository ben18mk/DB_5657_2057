WITH CanGo (src, dest) AS
(SELECT *
 FROM (SELECT DISTINCT Stp1.name AS src, Stp2.name AS dest
       FROM (SELECT Stp.*, St.name
             FROM stops Stp
             NATURAL JOIN stations St) AS Stp1,
            (SELECT Stp.*, St.name
             FROM stops Stp
             NATURAL JOIN stations St) AS Stp2
        WHERE Stp1.line_id = Stp2.line_id and Stp1.seq = Stp2.seq + 1) AS NewTable
 UNION ALL
 SELECT F1.src, F2.dest
 FROM CanGO F1, NewTable F2
 WHERE F1.dest = F2.src)
SELECT dest FROM CanGo WHERE src = @station_name