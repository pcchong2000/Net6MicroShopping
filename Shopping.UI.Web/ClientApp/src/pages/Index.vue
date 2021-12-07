<template>
  <el-container  class="container-body">

    <el-main>
      <el-row :gutter="20">
        <el-col :span="6"><div class="grid-content bg-purple"></div>1</el-col>
        <el-col :span="6"><div class="grid-content bg-purple"></div>2</el-col>
        <el-col :span="6"><div class="grid-content bg-purple"></div>3</el-col>
        <el-col :span="6"><div class="grid-content bg-purple"></div>4</el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script setup>
import productService from '../services/productService'

productService.getProductCategoryList({}); 
</script>

<style scoped>

</style>
