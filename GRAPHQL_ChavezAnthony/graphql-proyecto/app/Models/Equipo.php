<?php
 
namespace App\Models;
 
use Illuminate\Database\Eloquent\Model;
use App\Models\Jugador;
 
class Equipo extends Model
{
    protected $fillable = [
        'nombre',
        'ciudad',
        'estadio',
        'fundado_en',
        'director',
        'escudo_url'
    ];
 
    public function jugadores()
    {
        return $this->hasMany(Jugador::class);
    }
}