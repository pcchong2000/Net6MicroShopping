<template>
  <el-container  class="container-body">
    <el-header>
      <Header></Header>
    </el-header>
    <el-container class="container-main">
      <el-aside width="180px"><Menu></Menu></el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script >
import Menu from '../components/Menu.vue'
import Header from '../components/Header.vue'

export default {
  name: 'App',
  components: {
    Menu,
    Header
  },
  created(){

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
.container-body{
    height: 100%;
}
.container-main{
  height: 100%;
}
  .el-aside {
    background-color: #545c64;
    color: #333;
    text-align: center;
    line-height: 200px;
  }

  .el-main {
    background-color: #E9EEF3;
    color: #333;

  }

  body > .el-container {
    margin-bottom: 40px;
  }
</style>
<style>
.table-action{
  margin: 10px 0;
}
.table-search{
  border: 1px solid rgb(182, 182, 182);
  border-radius: 10px;
  padding: 0 10px;
  margin: 10px 0;
}
.table-search-left{
  padding-top: 20px;
}
.table-search-right{
  display: flex !important;
  align-items: center;
  justify-content: end;
}
</style>
