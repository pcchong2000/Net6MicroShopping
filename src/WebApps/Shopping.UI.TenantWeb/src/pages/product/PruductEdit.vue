<template>
  <div>
    <el-tabs type="card" @tab-click="handleClick">
      <el-tab-pane label="基本信息">
        <el-form ref="submitref" label-position="left" :model="submitData" label-width="120px">
          <el-form-item label="商品全站分类">
            <el-select v-model="submitData.productCategoryId" >
              <el-option
                v-for="item in productCategoryList"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
            </el-option>
          </el-select>
          </el-form-item>
          <el-form-item label="商品门店分类">
            <el-select v-model="submitData.storeProductCategoryId" >
              <el-option
                v-for="item in storeProductCategoryList"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
            </el-option>
          </el-select>
          </el-form-item>
          <el-form-item label="商品名称">
            <el-input v-model="submitData.name"></el-input>
          </el-form-item>
          <el-form-item label="编号">
            <el-input v-model="submitData.code"></el-input>
          </el-form-item>
          <el-form-item label="图片">
              <el-upload
                class="avatar-uploader"
                :action="uploadUrl"
                :show-file-list="false"
                :on-success="handleImageSuccess"
                
              >
                <el-image v-if="submitData.imageUrl" :src="submitData.imageUrl" fit="contain" class="avatar" />
                <el-icon v-else class="avatar-uploader-icon"><plus /></el-icon>
              </el-upload>

            <el-input v-model="submitData.imageUrl"></el-input>
          </el-form-item>
          <el-form-item label="价格">
            <el-input v-model="submitData.price"></el-input>
          </el-form-item>
          <el-form-item label="排序">
            <el-input v-model="submitData.sort"></el-input>
          </el-form-item>
          <el-form-item label="描述">
            <el-input v-model="submitData.description"></el-input>
          </el-form-item>
        </el-form>
      </el-tab-pane>
      <el-tab-pane label="商品规格">
        <el-table :data="productModelList" height="750" style="width: 100%">
          <el-table-column 
            v-for="(item,index) in modelCategoryList"
            :key="item.name"
            width="120">
            <template #header>
              <span>{{item.name}}</span>
              <el-button size="small" @click="deleteModelCategory(index)" >删除</el-button>
            </template>
            <template #default="scope">
              <el-input v-model="scope.row.value[item.code]"></el-input>
            </template>
          </el-table-column>
          <el-table-column label="库存" width="120">
            <template #default="scope">
              <el-input v-model="scope.row.number"></el-input>
            </template>
          </el-table-column>
          <el-table-column label="价格" width="120">
            <template #default="scope">
              <el-input v-model="scope.row.price"></el-input>
            </template>
          </el-table-column>
          <el-table-column label="操作" fixed="right" >
            <template #header>
              <el-button size="small" @click="showAddModelCategory" >新增规格类型</el-button>
              <el-button size="small" @click="addProductModel" >新增规格</el-button>
            </template>
            <template #default="scope">
              <el-button size="small" @click="deleteProductModel(scope.index)" >删除</el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-tab-pane>
    </el-tabs>
    <el-form ref="submitbtnref" label-position="left" label-width="120px">
      <el-form-item>
        <el-button  @click="cancel">取消</el-button>
        <el-button type="primary" @click="submit">确定</el-button>
      </el-form-item>
    </el-form>
    <el-dialog v-model="dialogModelCategoryVisible" title="添加商品型号类型">
      <el-form ref="modelCategoryRef" label-position="left" :model="submitModelCategory" label-width="80px">

        <el-form-item label="名称">
          <el-input v-model="submitModelCategory.name"></el-input>
        </el-form-item>
        <el-form-item label="编号">
          <el-input v-model="submitModelCategory.code"></el-input>
        </el-form-item>
        <el-form-item label="排序">
          <el-input v-model="submitModelCategory.sort"></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="submitModelCategory.description"></el-input>
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogModelCategoryVisible = false">取消</el-button>
          <el-button type="primary" @click="addModelCategory">确定</el-button>
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
      submitData:{productCategoryId:'',storeProductCategoryId:'',},
      productCategoryList:[],
      storeProductCategoryList:[],
      storeId:"",
      storeName:"",
      productId:"",
      uploadUrl:"",
      modelCategoryList:[],//型号名称  [{name:"大小",code:'size'},{name:"颜色",code:'color'}]
      productModelList:[],   //[{value:{size:28,color:'红'},peice:1000,number:100,description:""}]
      
      productModelTemplate:{value:{},price:0,number:0,description:""},
      modelCategoryTemplate:{name:"",code:''},
      
      submitModelCategory:{name:"",code:'',sort:0,description:""},
      dialogModelCategoryVisible:false,
    };
  },
  created(){
    this.productId=this.$route.query.id;
    this.storeId=localStorage.getItem("storeId");
    this.storeName=localStorage.getItem("storeName");
    this.uploadUrl=window.fileUploadApi;
    if(this.productId){
      this.getDetail();
    }
    
    this.getProductCategory();
    this.getStoreProductCategory();
  },
  methods:{
    getDetail(){
      let query={productId:this.productId};
      productService.getProductDetail(query).then(resp=>{
        console.log(resp);
        this.submitData=resp.data;
        this.modelCategoryList=resp.data.storeProductModelCategoryList;
        this.productModelList=resp.data.storeProductModelList;
        this.productModelList.forEach(item=>{
          item.value=JSON.parse(item.value)
        });
      });
    },
    getProductCategory(){
      productService.getProductCategoryList({pageIndex:1,pageSize:20}).then(a=>{
        console.log(a);
        this.productCategoryList=a.data.list;
      });
    },
    getStoreProductCategory(){
      productService.getStoreProductCategoryList({pageIndex:1,pageSize:20,storeId:this.storeId}).then(a=>{
        console.log(a);
        this.storeProductCategoryList=a.data.list;
      });
    },
    submit(){

      this.submitData.storeId=this.storeId;
      this.submitData.storeName=this.storeName;
      this.submitData.status=1;
      this.submitData.storeProductModelCategoryList=this.modelCategoryList;
      
      this.submitData.storeProductModelCategoryList.forEach(item=>{
        let valueItem=this.productModelList.map(a=>a.value[item.code]);
        let newvalueItem= [...new Set(valueItem)]
        item.items=JSON.stringify(newvalueItem);
      });

      let productModelList=JSON.parse(JSON.stringify(this.productModelList));
      productModelList.forEach(item=>{
        item.value=JSON.stringify(item.value)

      });
      this.submitData.storeProductModelList=productModelList;

      if(this.submitData.id){
        productService.putProduct(this.submitData).then(resp=>{
          this.dialogVisible=false;
          //this.getDataList();
        });

      }else{
        productService.postProduct(this.submitData).then(resp=>{
          this.dialogVisible=false;
          //this.getDataList();
        });
      }
      
    },
    cancel(){
      this.$router.go(-1);
    },
    handleImageSuccess(resp,file){
      console.log(resp);
      this.submitData.imageUrl=apiurl+resp[0].pathUrl;
    },
    showAddModelCategory(e,code){
      if(code){
        this.submitModelCategory=this.modelCategoryList.find(a=>a.code==code);
      }else{
        this.submitModelCategory=JSON.parse(JSON.stringify(this.modelCategoryTemplate));
      }
      this.dialogModelCategoryVisible=true;
    },
    addModelCategory(){
      this.modelCategoryList.push(this.submitModelCategory);
      this.productModelList.forEach(item=>{
        item.value[this.submitModelCategory.code]='';
      });
      this.dialogModelCategoryVisible=false;
    },
    deleteModelCategory(index){
      var modelCategory = this.modelCategoryList[index];
      if(modelCategory){
        this.modelCategoryList.splice(index,1);
        this.productModelList.forEach(item=>{
          delete item.value[modelCategory.code];
        });
      }
    },
    addProductModel(){
      let productModel=JSON.parse(JSON.stringify(this.productModelTemplate));
      this.modelCategoryList.forEach(item=>{
        productModel.value[item.code]='';
      });
      this.productModelList.push(productModel);
    },
    deleteProductModel(index){
      this.productModelList.splice(index,1);
    },
  }
}
</script>

<style scoped>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
