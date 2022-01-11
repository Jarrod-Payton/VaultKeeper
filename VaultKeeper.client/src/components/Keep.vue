<template>
  <div class="card mt-2 elevation-2 bg-dark action" @click="KeepModal">
    <img
      :src="keep.img"
      alt="Cover Image"
      class="pt-3 px-3 rounded"
      v-if="keep.img != `{{editKeepImg}}`"
    />
    <img
      src="../assets/img/KnightsIcon.jpg"
      class="px-3 pt-3 rounded"
      alt="Default"
      v-else
    />
    <div class="card-body">
      <div class="row">
        <div class="col-12">
          <div class="d-flex justify-content-center align-items-center pb-2">
            <div class="title">{{ keep.name }}</div>
          </div>
        </div>
        <div class="col-12">
          <div class="d-flex justify-content-between align-items-center p-1">
            <div class="description">{{ keep.description }}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { Modal } from "bootstrap"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    return {
      KeepModal() {
        try {
          keepsService.setActive(props.keep)
          Modal.getOrCreateInstance(document.getElementById("KeepModal")).toggle();
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      }
    }
  },
}
</script>
<style scoped>
.title {
  font-size: 2vh;
}
.description {
  font-size: 1.3vh;
}
</style>