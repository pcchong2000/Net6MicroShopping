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
        <el-sub-menu index="1">
          <template #title>
            <el-icon><location /></el-icon>
            <span>商品管理</span>
          </template>
          <el-menu-item index="/product/list">商品列表</el-menu-item>
          
        </el-sub-menu>
        <el-sub-menu  index="2">
          <template #title>
            <el-icon><location /></el-icon>
            <span>订单管理</span>
          </template>
          <el-menu-item index="/order/list">订单列表</el-menu-item>
          
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
