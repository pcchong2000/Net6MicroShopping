<template>
  <el-container  class="container-body">

    <el-main>
      <el-row :gutter="20">
        <el-col v-for="item in productCategorys" :key="item.id" :span="6">
          <div class="grid-content bg-purple">
            <router-link :to="'/product/category/'+item.id">{{item.name}}</router-link>
          </div>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script setup>
import { ref } from 'vue'
import productService from '../services/productService'
const productCategorys=ref([]);

productService.getProductCategoryList({}).then(resp=>{
productCategorys.value=resp.data.list;
//console.log(productCategorys)
});

</script>

<style scoped>

</style>
