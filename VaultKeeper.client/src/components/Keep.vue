<template>
  <div class="image action m-2" title="See Details" @click="KeepModal">
    <img :src="keep.img" alt="Keep Photo" class="actual-photo rounded" />
    <div class="text">
      <div class="row">
        <div class="col-12">
          <div class="d-flex justify-content-center align-items-center pb-2">
            <div class="title">{{ keep.name }}</div>
            <div class="creator">
              <img
                :src="keep.creator.picture"
                class="creator-pic"
                alt="Profile Pic"
              />
            </div>
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
import { vaultsService } from "../services/VaultsService"
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    return {
      KeepModal() {
        try {
          keepsService.setActive(props.keep)
          vaultsService.GetMyVaults()
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
img {
  max-width: 100%;
  display: block;
}
figure > img {
  grid-row: 1 / -1;
  grid-column: 1;
}
.creator {
  position: absolute;
  padding-top: 5px;
  right: 40px;
}
.creator-pic {
  height: 30px;
  border-radius: 50px;
}
.actual-photo {
  width: 100%;
  max-height: 70vh;
  object-fit: cover;
}
.text {
  position: absolute;
  bottom: 0;
  background: rgb(0, 0, 0);
  background: rgba(0, 0, 0, 0.5);
  color: #f1f1f1;
  width: 100%;
  padding: 20px;
}
.image {
  position: relative;
  max-width: 800px;
  margin: 0 auto;
}
.title {
  font-size: 2.4vh;
}
.description {
  font-size: 2vh;
}
</style>