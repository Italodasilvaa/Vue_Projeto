<template>
  <div>
    <titulo texto="Professores"  :btnVoltar="true"/>
    <div>
      <input
        type="text"
        placeholder="Nome do Professor"
        v-model="nome"
        @keyup.enter="addProfessor()"
      />
      <button class="btn btnInput" @click="addProfessor()">Adicionar</button>
    </div>
    <table>
      
      <thead>
        <th>Cod.</th>
        <th>Nome</th>
        <th>Materia</th>
        <th>Editar</th>
        <th>Remover</th>
        <th>Alunos</th>
      </thead>
      
      <tbody v-if="Professores.length">
        <tr v-for="(professor, index) in Professores" :key="index">
          <td class="colPequeno" style="text-align: center">{{ professor.id }}</td>
          
            <router-link :to="'/alunos/' + professor.id " 
            tag="td" style="cursor : pointer">
            {{ professor.nome }} 
              </router-link>

            <td  style="text-align: center">{{professor.materia}}</td>

            <router-link :to="`/professorDetalhe/${professor.id}` " 
            tag="td" style="cursor : pointer; text-align: center ; color: black ; font-weight: bold" @click="editar()" class=" colPequeno btnEditar">
            Editar
              </router-link>
              <td style="text-align: center " class="colPequeno" >
            <button class="btn btn_Danger" @click="remover(professor)">
              Remover
            </button>
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
      Alunos:[],
      nome: "",
      
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
          materia: professor.materia,
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
    },
    addProfessor() {
      let _professor = {
        nome: this.nome,
        materia: "",
        aluno:[]
      }
      this.$http
        .post("http://localhost:5000/api/professor", _professor)
        .then(res => res.json())
        .then(professor => this.professores.push(professor)), this.nome = "";
        
    },
    remover(professor) {
      
      this.$http.delete(`http://localhost:5000/api/professor/${professor.id}`)
      
    }
  }
};
</script>

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