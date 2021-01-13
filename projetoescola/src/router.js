import Vue from 'vue'
import Router from 'vue-router'
import Alunos from "./components/Aluno/Alunos";
import AlunoDetalhe from "./components/Aluno/AlunoDetalhe";
import ProfessorDetalhe from "./components/Professor/ProfessorDetalhe";
import Professor from "./components/Professor/Professor";
import Sobre from "./components/Sobre/Sobre";

Vue.use(Router);

export default new Router({
    routes: [
        {
            path:'/professores',
            nome:'Professores',
            component: Professor
        },
        {
            path:'/alunos/:prof_id',
            nome:'Alunos',
            component: Alunos
        },
        {
            path:'/alunosall',
            nome:'Alunos',
            component: Alunos
        },
        
        {
            path:'/AlunoDetalhe/:id',
            nome:'AlunoDetalhe',
            component: AlunoDetalhe
        },
        {
            path:'/ProfessorDetalhe/:professor_id',
            nome:'ProfessorDetalhe',
            component: ProfessorDetalhe
        },
        {
            path:'/sobre',
            nome:'Sobre',
            component: Sobre
        }
    ]
})