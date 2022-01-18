<template>
  <div>
    <div class="table-search">
      <el-row :gutter="20">
        <el-col :span="20" class="table-search-left">
          <el-form ref="login"  :inline="true" label-position="left" :model="searchData" >
            <el-form-item label="商品名称">
              <el-input v-model="searchData.name"></el-input>
            </el-form-item>
            <el-form-item label="编号">
              <el-input v-model="searchData.code"></el-input>
            </el-form-item>
            <el-form-item label="编号">
              <el-input v-model="searchData.code"></el-input>
            </el-form-item>
            <el-form-item label="编号">
              <el-input v-model="searchData.code"></el-input>
            </el-form-item>
          </el-form>
        </el-col>
        <el-col :span="4" class="table-search-right">
          <el-button type="primary" @click="getDataList">搜索</el-button>
        </el-col>
      </el-row>
    </div>
    <div class="table-action">
      <el-button  type="primary"  size="mini" @click="addClick">添加</el-button>
    </div>
    <div class="table-data">
      <el-table :data="dataList" border style="width: 100%">
        <el-table-column prop="imageUrl" label="图片" >
          <template #default="scope">
            <el-image :src="scope.row.imageUrl" fit="contain" class="avatar" >
              <template #error>
                <div class="image-slot">
                  <el-icon><icon-picture /></el-icon>
                </div>
              </template>
            </el-image>
          </template>
        </el-table-column>
        <el-table-column prop="name" label="名称" width="180" />
        <el-table-column prop="code" label="编号" width="180" />
        <el-table-column prop="productCategoryName" label="全站分类"  />
        <el-table-column prop="storeProductCategoryName" label="门店分类"  />
        <el-table-column prop="price" label="价格" />
        <el-table-column fixed="right" label="Operations" width="120">
          <template #default="scope">
            <el-button type="text" size="small" @click.prevent="editRow(scope.row)">编辑</el-button>
            <el-button type="text" size="small" @click.prevent="deleteRow(scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<script >
import  productService  from '../../services/productService'
export default {
  name: 'UserList',

  data:()=>{
    return {
      dataList:[],
      searchData:{
        pageIndex:1,
        pageSize:10,
      },
      storeId:"",
      storeName:"",
    };
  },
  created(){
    this.storeId=localStorage.getItem("storeId");
    this.storeName=localStorage.getItem("storeName");
    this.getDataList();
  },
  methods:{
    getDataList(){
      productService.getProductList(this.searchData).then(a=>{
        console.log(a);
        this.dataList=a.list;
      });
    },

    addClick(){
      this.$router.push("/product/edit");
    },

    editRow(item){
      this.$router.push("/product/edit?id="+item.id);
    },
    deleteRow(item){
      productService.deleteProduct({productId:item.id}).then(a=>{
        console.log(a);
        this.getDataList();
      });
    }
  }
}
</script>

<style scoped>
.avatar {
  width: 100px;
  height: 100px;
}
</style>
