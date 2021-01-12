<template>
  <div>
    <titulo texto="Professores" btnVoltar="true"/>
    <table>
      <thead>
        <th>Cod.</th>
        <th>Nome</th>
        <th>Alunos</th>
      </thead>
      <tbody v-if="Professores.length">
        <tr v-for="(professor, index) in Professores" :key="index">
          <td class="colPequeno" style="text-align: center">{{ professor.id }}</td>
          
            <router-link :to="'/alunos/' + professor.id " 
            tag="td" style="cursor : pointer">
            {{ professor.nome }} {{ professor.sobrenome }}
              </router-link>
          
          <td class="colPequeno" style="text-align: center">
            {{professor.qtdAlunos}}
          </td>
        </tr>
      </tbody>
      <tfoot v-else>
        <tr>
        <td colspan="3" style="text-align: center"> <h5>Nenhum Professor Encontrado</h5>
        </td>
      </tr>
      </tfoot>
    </table>
  </div>
</template>

<script>
import Titulo from "../_share/Titulo";

export default {
  components: {
    Titulo,
  },
  data() {
    return {
      Professores: [],
      Alunos:[]
    };
  },
  created() {
    this.$http
      .get("http://localhost:5000/api/aluno")
      .then((res) => res.json())
      .then((alunos) => {//apos o create carrega todos alunos 
        this.Alunos = alunos;
      this.carregarProfessores();//agora eu carrego os professores com o valor que veio de alunos
      });
  },
  props: {},
  methods:{
    pegarQtaAlunosPorProfessor() {
      this.Professores.forEach((professor, index) => {//para cada professor existente em Professores 
        professor = {//PEGO id e nome e depois filtro
          id: professor.id,
          nome: professor.nome,
          qtdAlunos: this.Alunos.filter(aluno => aluno.professor.id == professor.id).length
          //fazer o erofunction para analizar se alunos.professor.id e igual a professor id 
        }
        this.Professores[index] = professor;
      });

    },
    carregarProfessores(){
      this.$http
      .get("http://localhost:5000/api/professor")//pegou todos professores 
      .then((res) => res.json())
      .then(professor => {
        this.Professores = professor
        this.pegarQtaAlunosPorProfessor();// aqui eu chamo outra funcao 
        });
    }
  }
};
</script>

<style scoped>

</style>