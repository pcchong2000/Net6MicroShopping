<template>
  <div class="menu-container">
    <el-menu
        active-text-color="#ffd04b"
        background-color="#545c64"
        class="el-menu-vertical-demo"
        default-active="2"
        text-color="#fff"
        :router="true"
        @open="handleOpen"
        @close="handleClose"
      >
        <el-sub-menu>
          <template #title>
            <el-icon><location /></el-icon>
            <span>门店管理</span>
          </template>
          <el-menu-item index="/store/list">门店列表</el-menu-item>
          <el-menu-item index="/store/list1">门店列表1</el-menu-item>
        </el-sub-menu>
      </el-menu>
  </div>
</template>

<script >
export default {
  name: 'Menu',
  created(){
    this.getMenuData();
  },
  methods:{
    getMenuData(){
      
    }
  }
}
</script>

<style scoped>
.el-menu{
  border-right: none;
}
</style>
