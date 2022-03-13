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
          </el-form>
        </el-col>
        <el-col :span="4" class="table-search-right">
          <el-button type="primary" @click="getDataList">搜索</el-button>
        </el-col>
      </el-row>
    </div>
    <div class="table-action">
      <el-button  type="info"  size="mini" @click="dialogVisible = true">添加</el-button>
    </div>
    <div class="table-data">
      <el-table :data="dataList" border style="width: 100%">
        <el-table-column prop="imageUrl" label="图片" width="180" />
        <el-table-column prop="name" label="名称" width="180" />
        <el-table-column prop="sort" label="排序" width="180" />
        <el-table-column prop="description" label="描述" />
      </el-table>
    </div>
    <el-dialog v-model="dialogVisible" title="添加">
      <el-form ref="login" label-position="left" :model="submitData" label-width="80px">

        <el-form-item label="商品名称">
          <el-input v-model="submitData.name"></el-input>
        </el-form-item>
        <el-form-item label="排序">
          <el-input v-model="submitData.sort"></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="submitData.description"></el-input>
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="submit">确定</el-button>
        </span>
      </template>
    </el-dialog>
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
        storeId:"",
      },
      dialogVisible:false,
      submitData:{},
      storeId:"",
      storeName:"",
    };
  },
  created(){
    this.searchData.storeId=this.storeId=localStorage.getItem("storeId");
    this.storeName=localStorage.getItem("storeName");
    this.getDataList();
  },
  methods:{
    getDataList(){
      productService.getStoreProductCategoryList(this.searchData).then(a=>{
        console.log(a);
        this.dataList=a.list;
      });
    },
    submit(){

      this.submitData.storeId=this.storeId;

      productService.postStoreProductCategory(this.submitData).then(resp=>{
        this.dialogVisible=false;
        this.getDataList();
      });
    }
  }
}
</script>

<style scoped>

</style>
