import{D as p,c,a as d,o as e,b as a,d as t,F as l,q as m,E as g,e as u}from"./vendor.2a972c33.js";import{_ as v,A as s,k as f,P as k,l as y}from"./index.70b0ca1f.js";import{r as h}from"./ResetService.6fdd23e1.js";const x={setup(){return document.title="Keeper | Home",p(async()=>{try{s.loading=!0,await h.ResetEverything(),await f.GetAllKeeps(),s.loading=!1}catch(o){k.toast(o.message,"error"),y.error(o.message)}}),{keeps:c(()=>s.keeps),loading:c(()=>s.loading)}}},P={key:0,class:"Home-Page"},B={class:"row m-0 mt-1"},H={class:"col-12"},K={class:"container-masonry"};function S(o,b,j,r,w,A){const i=d("Keep"),_=d("Loading");return r.loading?(e(),g(_,{key:1})):(e(),a("div",P,[t("div",B,[t("div",H,[t("div",K,[(e(!0),a(l,null,m(r.keeps,n=>(e(),a("figure",{key:n.id},[u(i,{keep:n},null,8,["keep"])]))),128))])])])]))}var N=v(x,[["render",S],["__scopeId","data-v-7eb2fade"]]);export{N as default};
