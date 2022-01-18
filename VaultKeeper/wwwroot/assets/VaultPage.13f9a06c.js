import{C as y,u as f,D as k,c as d,a as m,o as a,b as o,d as t,t as u,l as v,F as V,q as w,E as b,e as I,p as P,i as K}from"./vendor.2a972c33.js";import{_ as S,A as s,l as p,v as _,P as l,b as R}from"./index.70b0ca1f.js";import{r as x}from"./ResetService.6fdd23e1.js";const C={setup(){const c=y(),i=f();return k(async()=>{try{s.loading=!0,await x.ResetEverything(),p.log(c.params.vaultId),await _.setupVaultPage(c.params.vaultId),await _.GetMyVaults(),s.activeVault.name==null&&(i.push({name:"Home"}),l.toast("this is a private album and as such you cannot view it")),s.loading=!1}catch(n){l.toast("You are not authorized good sir","error"),p.error(n.message),i.push({name:"Home"})}}),{RouteToCreatorPage(){var n;i.push({name:"Account",params:{accountId:(n=s.activeVault.creator)==null?void 0:n.id}})},RouteHome(){i.push({name:"Home"})},async removeKeepFromVault(n,e){await l.confirm("are you sure you want to remove this? It won't delete the keep but it might be hard to find again")&&await R.removeKeepFromVault(n,e)},async deleteVault(){await l.confirm("Are you sure you would like to delete this amazing vault?")&&(await _.deleteVault(c.params.vaultId),i.push({name:"Account",params:{accountId:s.account.id}}),l.toast("Deleted"))},account:d(()=>s.account),loading:d(()=>s.loading),keeps:d(()=>s.keeps),vault:d(()=>s.activeVault)}}},A=c=>(P("data-v-0a586284"),c=c(),K(),c),j={key:0,class:"VaultPage"},F={class:"row m-0"},B={class:"col-12"},D={class:"d-flex align-items-center justify-content-between"},H={class:"info"},N={class:"title"},E={class:"description"},L={class:"stats"},z={key:0,class:"privacy"},M={key:0,class:"stats"},T={key:1,class:"stats"},Y={key:0,class:"delete"},q={key:0,class:"row"},G={class:"row"},J={class:"col-12"},O={key:0,class:"col-12"},Q=["onClick"],U={key:1,class:"row"},W=A(()=>t("div",{class:"col-12"},[t("div",{class:"d-flex align-items-center justify-content-center"},[t("div",{class:"no-keeps"},"There Are No Keeps in this Vault Yet")])],-1)),X=[W];function Z(c,i,n,e,$,ee){const h=m("Keep"),g=m("Loading");return e.loading?(a(),b(g,{key:1})):(a(),o("div",j,[t("div",F,[t("div",B,[t("div",D,[t("div",H,[t("div",N,u(e.vault.name),1),t("div",E,u(e.vault.description),1),t("div",L,"Keeps: "+u(e.keeps.length),1),e.vault.creatorId==e.account.id?(a(),o("div",z,[e.vault.isPrivate?(a(),o("div",M,"Private")):(a(),o("div",T,"Public"))])):v("",!0)]),e.vault.creatorId==e.account.id?(a(),o("div",Y,[t("button",{class:"btn btn-outline-dark",onClick:i[0]||(i[0]=r=>e.deleteVault())}," Delete Vault ")])):v("",!0)])])]),e.keeps.length>0?(a(),o("div",q,[(a(!0),o(V,null,w(e.keeps,r=>(a(),o("div",{class:"col-md-4 col-sm-6 col-lg-3 m-4",key:r.id},[t("div",G,[t("div",J,[I(h,{keep:r},null,8,["keep"])]),e.vault.creatorId==e.account.id?(a(),o("div",O,[t("button",{class:"btn w-100 btn-danger mt-1",title:"Remove keep from vault",onClick:te=>e.removeKeepFromVault(r.vaultKeepId,r.id)}," Remove from Vault ",8,Q)])):v("",!0)])]))),128))])):(a(),o("div",U,X))]))}var ce=S(C,[["render",Z],["__scopeId","data-v-0a586284"]]);export{ce as default};
