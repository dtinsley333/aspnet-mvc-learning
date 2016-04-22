select ani.CommonName,
ani.Gender,
ani.Species,
ani.AcquiredDate,
h.Name, h.[Description]
from dbo.Animals ani
join dbo.Habitat h
on ani.HabitatId=h.HabitatId
order by ani.CommonName