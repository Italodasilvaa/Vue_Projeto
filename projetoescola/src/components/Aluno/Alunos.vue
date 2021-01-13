<template>
  <div>
    <titulo
      :texto="
        professorId != undefined
          ? 'Professor: ' + this.professor.nome
          : 'Todos os Alunos'
      "
      :btnVoltar="true"
    />

    <div v-if="professorId != undefined">
      <input
        type="text"
        placeholder="Nome do Aluno"
        v-model="nome"
        @keyup.enter="addAluno()"
      />
      <button class="btn btnInput" @click="addAluno()">Adicionar</button>
    </div>
    <table>
      <thead>
        <th>Mat.</th>
        <th>Nome</th>
        <th class="colPequeno">Opções</th>
      </thead>
      <tbody v-if="alunos.length">
        <tr v-for="(aluno, index) in alunos" :key="index">
          <td class="colPequeno" style="text-align: center"> {{ aluno.alunoId }}</td>
          
          <router-link
            :to="`/alunoDetalhe/${aluno.alunoId}`"
            tag="td"
            style="cursor: pointer"
          >
            {{ aluno.nome }} {{ aluno.sobrenome }}
          </router-link>

          <td style="text-align: center " >
            <button class="btn btn_Danger" @click="remover(aluno)">
              Remover
            </button>
          </td>
        </tr>
      </tbody>
      
      <tfoot v-else>
        <tr>
        <td colspan="3" style="text-align: center"> <h5>Nenhum Aluno Encontrado</h5>
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
      titulo: "Aluno",
      professorId: this.$route.params.prof_id,      
      professor: {},
      nome: "",
      alunos: [],
      
    };
  },
  created() {
    if (this.professorId) {
      this.carregarProfessores();
      this.$http
        .get(`http://localhost:5000/api/aluno/ByProfessor/${this.professorId}`  )
        .then((res) => res.json())
        .then((alunos) => (this.alunos = alunos));
        console.log(this.alunos)
    } else {
      this.$http
        .get("http://localhost:5000/api/aluno")
        .then((res) => res.json())
        .then((alunos) => (this.alunos = alunos));
    }
  },
  props: {},

  methods: {
    addAluno() {
      let _aluno = {
        nome: this.nome,
        sobrenome: "",
        dataNascimento : "",
        professorid: this.professor.id,
      };
      this.$http
        .post("http://localhost:5000/api/aluno", _aluno)
        .then(res => res.json())
        .then(aluno => this.alunos.push(aluno)), 
        this.nome = "";
    },
    remover(aluno) {
      
      this.$http.delete(`http://localhost:5000/api/aluno/${aluno.alunoId}`).then(() => {
        let indice = this.alunos.indexOf(aluno);
        this.alunos.splice(indice, 1);
      });
    },
    carregarProfessores() {
      this.$http
        .get(`http://localhost:5000/api/professor/${this.professorId}`) //pegou todos professores
        .then((res) => res.json())
        .then((professor) => {
          this.professor = professor;
        });
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
input {
  width: calc(100% - 150px);
  padding: 10px;
  font-size: 1.3em;
  border: 0px;
  display: inline;
}
.btnInput {
  width: 120px;
  background-color: #5a5a5a;
  padding: 15px;
  display: inline;
  margin-left: 5px;
  border: 0;
}
</style>
