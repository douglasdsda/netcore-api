import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AlunosComponent } from './components/alunos/alunos.component';
import { DashbordComponent } from './components/dashbord/dashbord.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { ProfessorDetalheComponent } from './components/professores/professor-detalhe/professor-detalhe.component';
import { ProfessoresComponent } from './components/professores/professores.component';

const routes: Routes = [
  { path: 'alunos', component: AlunosComponent },
  { path: 'alunos/:id', component: AlunosComponent },
  { path: 'professores', component: ProfessoresComponent },
  { path: 'professor/:id', component: ProfessorDetalheComponent },
  { path: 'perfil', component: PerfilComponent },
  { path: 'dashbord', component: DashbordComponent },
  { path: '', redirectTo: 'dashbrod', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashbrod', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
