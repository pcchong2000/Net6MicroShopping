<template>
  <el-container  class="container-body">
    <el-header>
      <Header></Header>
    </el-header>
    <el-main>
      <el-row :gutter="20">
        <el-col :span="6"  v-for="item in storeList" :key="item.id" @click="onClickStore(item)" >
          <div class="grid-content bg-purple">
            {{item.name}}
          </div>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script >
import Header from '../../components/Header.vue'
import  storeService  from '../../services/storeService'
export default {
  name: 'UserList',
  components: {
    Header
  },
  data:()=>{
    return {
      storeList:[],
      pageRequest:{
        pageIndex:1,
        pageSize:10,
      },
    };
  },
  created(){
    storeService.getList(this.pageRequest).then(resp=>{
      console.log(resp);
      this.storeList=resp.list;
    });

  },
  methods:{
    onClickStore(item){
      console.log(item);
      localStorage.setItem("storeId",item.id);
      localStorage.setItem("storeName",item.name);
      this.$router.push("/index");
    }
  }
}
</script>

<style scoped>
.el-header, .el-footer {
    background-color: #B3C0D1;
    color: #333;
    text-align: center;
    line-height: 60px;
  }
</style>
